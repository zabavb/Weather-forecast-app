using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Models;
using UserAPI.Models.Auth;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoints for performing operations on users, including:
    /// - Retrieving a specific user by ID.
    /// - Creating users.
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </remarks>
    /// <param name="userService">Service for user operations.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService, IOptions<JwtSettings> jwtOptions) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user with the specified ID.</returns>
        /// <response code="200">Returns the user if found.</response>
        /// <response code="404">If the user with the specified ID is not found or ID was not specified.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (id.Equals(Guid.Empty))
                    return NotFound($"User ID [{id}] was not provided.");

                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token upon successful authentication.
        /// </summary>
        /// <param name="request">The login request containing user credentials (username or email and password).</param>
        /// <returns>
        /// - <c>200 OK</c>: If authentication is successful, returns a JWT token along with user details.<br/>
        /// - <c>400 Bad Request</c>: If the request model is invalid.<br/>
        /// - <c>401 Unauthorized</c>: If the provided credentials are incorrect.
        /// </returns>
        /// <response code="200">Returns the JWT token and authenticated user details.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="401">If authentication fails due to incorrect credentials.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User? user = await _userService.AuthenticateAsync(request);
            if (user == null)
                return Unauthorized("Invalid username/email or password.");

            var token = GenerateJwtToken(user);
            return Ok(new
            {
                Token = token,
                ExpiresIn = _jwtSettings.ExpiresInMinutes,
                User = new { user.UserId, user.Username }
            });
        }

        /// <summary>
        /// Registers a new user and stores their credentials securely.
        /// </summary>
        /// <param name="request">The registration request containing user details.</param>
        /// <returns>
        /// - <c>201 Created</c>: If registration is successful.<br/>
        /// - <c>400 Bad Request</c>: If the request data is invalid.<br/>
        /// - <c>500 Internal Server Error</c>: If an unexpected error occurs.
        /// </returns>
        /// <response code="201">Returns the registered user details.</response>
        /// <response code="400">If the provided registration data is invalid.</response>
        /// <response code="500">If an unexpected error occurs.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.RegisterAsync(request);
                return Created(nameof(Register), request);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Generates a JWT token for an authenticated user.
        /// </summary>
        /// <param name="user">The user object for whom the token is generated.</param>
        /// <returns>A JWT token string containing encoded user claims.</returns>
        /// <remarks>
        /// This method generates a JWT token that includes:
        /// - User ID (`NameIdentifier`)
        /// - Username (`Name`)
        /// - Email (`Email address`)
        /// </remarks>
        [NonAction]
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

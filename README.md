<p align="center">
  <img src="client/src/assets/logo.svg" alt="Weather App Logo" width="200"/>
</p>

<h1 align="center">Weather Forecast Web Application</h1>

<p>
  <strong>Weather Forecast App</strong> is a modern full-stack web application that allows users 
  to retrieve and search weather forecasts for today or the next three days across various cities. 
  Built using a microservices architecture, the app showcases key backend and frontend practices 
  like RESTful APIs, authentication, caching, asynchronous programming, and validation.
</p>

<p>
  This project demonstrates integration with a remote weather API and efficient data handling 
  via Redis caching, database persistence, and optimized frontend performance.
</p>

<h2>🧱 Architecture Overview</h2>
<ul>
  <li>
    <strong>Backend:</strong> ASP.NET Core 8 with a microservice architecture
  </li>
  <li>
    <strong>Frontend:</strong> React with TypeScript
  </li>
  <li>
    <strong>Gateway:</strong> Ocelot API Gateway
  </li>
</ul>

<h2>🔙 Backend Details</h2>
<h3>Microservices</h3>
<ul>
  <li>
    <strong>UserAPI</strong> – Manages user accounts and authentication:
    <ul>
      <li>
        JWT-based role authentication
      </li>
      <li>
        EF Core-based interaction with SQL database
      </li>
      <li>
        Includes login and registration flows
      </li>
    </ul>
  </li>
  <li>
    <strong>ForecastAPI</strong> – Interacts with a remote <code>weather.api</code> service to fetch:
    <ul>
      <li>
        Today’s weather forecast
      </li>
      <li>
        Forecasts for the next 3 days (inclusive of today)
      </li>
      <li>
        Filtering based on city
      </li>
    </ul>
  </li>
  <li>
    <strong>Gateway</strong> – Ocelot-based API Gateway to route requests between services securely and efficiently
  </li>
</ul>

<h3>Common Backend Features</h3>
<ul>
  <li>
    RESTful API structure using controller-service-repository pattern
  </li>
  <li>
    JWT Authentication and Authorization
  </li>
  <li>
    Redis caching for optimized forecast responses
  </li>
  <li>
    Asynchronous programming (async/await) for non-blocking operations
  </li>
  <li>
    Serilog for structured logging
  </li>
  <li>
    Validation of incoming data
  </li>
</ul>

<h2>🖥️ Frontend Details</h2>
<h3>Architecture</h3>
<p>
  <code>api → repository → service → container → component</code>
</p>
<h3>Main Features</h3>
<ul>
  <li>
    React with TypeScript, Vite as the build tool
  </li>
  <li>
    JWT token handling and role-based UI updates
  </li>
  <li>
    Axios for HTTP requests to backend services
  </li>
  <li>
    Zod for form and schema validation
  </li>
  <li>
    Memoization of expensive operations for performance
  </li>
</ul>

<h2>🔐 Authentication & Authorization</h2>
<ul>
  <li>
    Secure login and registration
  </li>
  <li>
    JWT-based authentication with protected routes
  </li>
  <li>
    Tokens stored in <code>localStorage</code>
  </li>
</ul>

<h2>🛠️ Technologies Used</h2>
<h3>Backend</h3>
<ul>
  <li>ASP.NET Core 8</li>
  <li>Entity Framework Core</li>
  <li>Redis</li>
  <li>Ocelot API Gateway</li>
  <li>Serilog</li>
</ul>
<h3>Frontend</h3>
<ul>
  <li>React + TypeScript</li>
  <li>Vite</li>
  <li>Zod</li>
  <li>Axios</li>
</ul>

<h2>📡 API Integration</h2>
<p>This project consumes a remote third-party API for weather data:</p>
<ul>
  <li>All forecast data is retrieved from the 
    <a href="www.weatherapi.com">www.weatherapi.com</a>
    remote server</li>
  <li>Data is filtered and cached appropriately to reduce unnecessary API calls</li>
</ul>

<h2>🧪 Development Status</h2>
<p>
  <em>
    This project is a demonstration of microservice integration, JWT security, and caching. Payment systems 
    or admin panels are not part of the scope. Future improvements may include user dashboards, forecast 
    history, and enhanced UX features.
  </em>
</p>
<h2>🔗 Repository</h2>
<p> GitHub: 
  <a href="github.com/zabavb/weather-forecast-app">github.com/zabavb/weather-forecast-app</a>
</p>

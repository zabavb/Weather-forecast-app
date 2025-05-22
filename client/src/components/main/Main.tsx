import { Weather, WeatherRequest } from '../../types';

interface MainProps {
  isAuthenticated: boolean;
  onNavigate: (path: string) => void;
  weather: Weather | null;
  days: number[];
  defaultLocation: string;
  defaultDays: number;
  onSearch: (request: WeatherRequest) => Promise<void>;
}

const Main: React.FC<MainProps> = ({
  isAuthenticated,
  onNavigate,
  weather,
  days,
  defaultLocation,
  defaultDays,
  onSearch,
}) => {
  return (
    <>
      {isAuthenticated ? (
        <></>
      ) : (
        <button onClick={() => onNavigate('/login')}>Login</button>
      )}
      <button onClick={() => onNavigate('/profile')}>My Profile</button>
      <h1>Main Page</h1>

      <div>
        <form
          onSubmit={(e) => {
            e.preventDefault();
            const formData = new FormData(e.currentTarget);
            const location = formData.get('location') as string;
            const days = Number(formData.get('days'));
            onSearch({ location, days });
          }}
        >
          <input
            type='text'
            name='location'
            defaultValue={defaultLocation}
            placeholder='Location...'
          />
          <p>
            <span>For days:</span>
            {days.map((number) => (
              <label key={number}>
                <input
                  type='radio'
                  name='days'
                  defaultChecked={defaultDays === number}
                  value={number}
                />
                {number}
              </label>
            ))}
          </p>
          <button type='submit'>Search</button>
        </form>
      </div>

      {weather ? (
        <div>
          <h2>
            {weather.location.name}, {weather.location.country}
          </h2>
          <p>{weather.location.region}</p>

          <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
            <img
              src={weather.current.condition.icon}
              alt={weather.current.condition.text}
            />
            <h3>{weather.current.condition.text}</h3>
          </div>

          <p>
            Temperature: {weather.current.temp_c}°C / {weather.current.temp_f}
            °F
          </p>
          <p>Wind Speed: {weather.current.wind_kph} kph</p>
          <p>Humidity: {weather.current.humidity}%</p>

          <h3>Forecast:</h3>
          <table>
            <thead>
              <tr>
                <th>Date</th>
                <th>Condition</th>
                <th>Max Temp (°C)</th>
                <th>Min Temp (°C)</th>
              </tr>
            </thead>
            <tbody>
              {weather.forecast.forecastday.map((day) => (
                <tr key={day.date}>
                  <td>{day.date}</td>
                  <td>
                    <img
                      src={day.day.condition.icon}
                      alt={day.day.condition.text}
                    />{' '}
                    {day.day.condition.text}
                  </td>
                  <td>{day.day.maxtemp_c}°C</td>
                  <td>{day.day.mintemp_c}°C</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <h3>Loading...</h3>
      )}
    </>
  );
};

export default Main;

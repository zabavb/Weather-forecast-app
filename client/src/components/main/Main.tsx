import { Dispatch, SetStateAction } from 'react';
import { Weather } from '../../types';

interface MainProps {
  isAuthenticated: boolean;
  onNavigate: (path: string) => void;
  onChangeLocation: Dispatch<SetStateAction<string>>;
  weather: Weather | null;
  onSearch: () => Promise<void>;
  defaultLocation: string;
}

const Main: React.FC<MainProps> = ({
  isAuthenticated,
  onNavigate,
  onChangeLocation,
  weather,
  onSearch,
  defaultLocation,
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
        <input
          type='text'
          onChange={(e) => onChangeLocation(e.target.value)}
          defaultValue={defaultLocation}
          placeholder='Location...'
        />
        <button onClick={onSearch}>Search</button>
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

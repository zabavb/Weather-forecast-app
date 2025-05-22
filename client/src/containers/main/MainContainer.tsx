import { useNavigate } from 'react-router-dom';
import { useEffect, useState, useMemo } from 'react';
import { weatherService } from '../../services/weatherService';
import { Weather } from '../../types/weather/Weather';
import Main from '../../components/main/Main';
import { WeatherRequest } from '../../types';

const MainContainer: React.FC = () => {
  const isAuth = localStorage.getItem('token') ? true : false;
  const navigate = useNavigate();
  const days = useMemo<number[]>(() => [1, 3], []);
  const defaultRequest = useMemo<WeatherRequest>(
    () => ({
      location: 'London',
      days: days[0],
    }),
    [days],
  );

  const [weather, setWeather] = useState<Weather | null>(null);
  const handleNavigate = (route: string) => navigate(route);

  useEffect(() => {
    (async () => await handleSearch(defaultRequest))();
  }, [defaultRequest]);

  const handleSearch = async (request: WeatherRequest): Promise<void> => {
    try {
      setWeather(null);
      const response = await weatherService(request);
      setWeather(response);
    } catch (error) {
      console.log('Error fatching weather data:', error);
    }
  };

  return (
    <Main
      isAuthenticated={isAuth}
      onNavigate={handleNavigate}
      weather={weather}
      days={days}
      defaultLocation={defaultRequest.location}
      defaultDays={defaultRequest.days}
      onSearch={handleSearch}
    />
  );
};

export default MainContainer;

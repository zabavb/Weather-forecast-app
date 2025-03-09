import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { weatherService } from '../../services/weatherService';
import { Weather } from '../../types/weather/Weather';
import Main from '../../components/main/Main';

const MainContainer: React.FC = () => {
  const isAuth = localStorage.getItem('token') ? true : false;
  const navigate = useNavigate();
  const [location, setLocation] = useState<string>('London');
  const twoWeeks = 14;
  const [weather, setWeather] = useState<Weather | null>(null);
  const handleNavigate = (route: string) => navigate(route);

  useEffect(() => {
    (async () => setWeather(await weatherService(location, twoWeeks)))();
  }, []);

  const handleSearch = async (): Promise<void> => {
    try {
      setWeather(null);
      const response = await weatherService(location, twoWeeks);
      setWeather(response);
    } catch (error) {
      console.log('Error fatching weather data:', error);
    }
  };

  return (
    <Main
      isAuthenticated={isAuth}
      onNavigate={handleNavigate}
      onChangeLocation={setLocation}
      weather={weather}
      onSearch={handleSearch}
      defaultLocation={location}
    />
  );
};

export default MainContainer;

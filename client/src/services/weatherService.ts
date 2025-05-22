import axios from 'axios';
import { WEATHER } from '../api';
import { Weather, WeatherRequest } from '../types';

/**
 * Retrievers weather forecast data based on provided parameters
 */
export const weatherService = async (
  request: WeatherRequest,
): Promise<Weather> => {
  const url = WEATHER(request.location, request.days);

  try {
    const response = await axios.get<Weather>(url);
    return response.data;
  } catch (error) {
    console.error(`API Call Failed: ${url}`, error);
    throw new Error('An error occurred. Please try again later.');
  }
};

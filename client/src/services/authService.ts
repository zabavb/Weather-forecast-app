import axios from 'axios';
import { JwtResponse, Login, Register } from '../types';
import { LOGIN, REGISTER } from '../api';

/**
 * Generic function to handle API requests with error handling.
 */
const apiCall = async (
  method: 'get' | 'post',
  url: string,
  data?: object,
  token?: string,
): Promise<JwtResponse> => {
  try {
    const headers = token ? { Authorization: `Bearer ${token}` } : {};
    const response = await axios({ method, url, data, headers });
    return response.data;
  } catch (error) {
    console.error(`API Call Failed: ${method.toUpperCase()} ${url}`, error);
    throw new Error('An error occurred. Please try again later.');
  }
};

/**
 * Logs in the user and stores the token.
 */
export const loginService = async (data: Login): Promise<JwtResponse> =>
  await apiCall('post', LOGIN, data);

/**
 * Registers a new user.
 */
export const registerService = async (data: Register): Promise<JwtResponse> =>
  await apiCall('post', REGISTER, data);

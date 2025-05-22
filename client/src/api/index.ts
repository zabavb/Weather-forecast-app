import { API_ROUTES } from './config/apiConfig';

// ================= AUTHENTICATION =================

export const LOGIN = API_ROUTES.AUTH.LOGIN;
export const REGISTER = API_ROUTES.AUTH.REGISTER;
export const GET_USER_BY_ID = API_ROUTES.AUTH.GET_BY_ID;

// ================= WEATHER =================

export const WEATHER = (location: string = 'London', days: number = 14) =>
  API_ROUTES.WEATHER(location, days);

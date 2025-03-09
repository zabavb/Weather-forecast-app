const GATEWAY = `https://localhost:7241/gateway`;
const AUTH = `${GATEWAY}/auth`;
const WEATHER = `${GATEWAY}/weather`;

export const API_ROUTES = {
  AUTH: {
    GET_BY_ID: (id: string) => `${AUTH}/${id}`,
    LOGIN: `${AUTH}/login`,
    REGISTER: `${AUTH}/register`,
  },
  WEATHER: (location: string, days: number) => `${WEATHER}/${location}/${days}`,
};

const BASE_URL = (port: number) => `https://localhost:${port}/api`

const USER_API_BASE_URL = BASE_URL(7289)

export const API_ROUTES = {
	AUTH: {
		LOGIN: `${USER_API_BASE_URL}/users/login`,
		REGISTER: `${USER_API_BASE_URL}/users/register`,
		GET_BY_ID: (id: string) => `${USER_API_BASE_URL}/users/${id}`,
	},
}

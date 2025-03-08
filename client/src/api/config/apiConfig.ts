const GATEWAY = `https://localhost:7241/gateway`

export const API_ROUTES = {
	AUTH: {
		GET_BY_ID: (id: string) => `${GATEWAY}/auth/${id}`,
		LOGIN: `${GATEWAY}/auth/login`,
		REGISTER: `${GATEWAY}/auth/register`,
	},
}

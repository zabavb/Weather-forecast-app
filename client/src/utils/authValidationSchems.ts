import { z } from "zod"

export const loginSchema = z.object({
	identifier: z
		.string()
		.min(4, "Username or Email must be at least 4 characters.")
		.max(20, "Username or Email must be at most 20 characters.")
		.refine((val) => /^[\w\W]{4,20}$/.test(val) || /\S+@\S+\.\S+/.test(val), {
			message: "Invalid Username or Email.",
		}),
	password: z
		.string()
		.min(6, "Password must be at least 6 characters.")
		.refine((val) => /^(?=.*[A-Za-z])(?=.*\d).{6,}$/.test(val), {
			message:
				"Password must be at least 6 characters long and contain at least one letter and one number.",
		}),
})

export type LoginFormData = z.infer<typeof loginSchema>

export const registerSchema = z
	.object({
		username: z
			.string()
			.min(4, "Username must be at least 4 characters.")
			.max(20, "Username too long."),
		email: z.string().email("Invalid email.").optional().or(z.literal("")),
		password: z
			.string()
			.min(6, "Password must be at least 6 characters.")
			.refine((val) => /^(?=.*[A-Za-z])(?=.*\d).{6,}$/.test(val), {
				message:
					"Password must be at least 6 characters long and contain at least one letter and one number.",
			}),
		passwordConfirm: z.string(),
	})
	.refine((data) => data.username !== "" || data.email !== "", {
		message: "Either Username or Email must be provided.",
		path: ["username"],
	})
	.refine((data) => data.password === data.passwordConfirm, {
		message: "Passwords do not match.",
	})

export type RegisterFormData = z.infer<typeof registerSchema>

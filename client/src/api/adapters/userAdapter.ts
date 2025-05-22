import { RegisterRequest } from '../../types';
import { RegisterFormData } from '../../utils';

export const fromRegisterFormToRegisterRequest = (
  form: RegisterFormData,
): RegisterRequest => ({
  username: form.username,
  email: form.email,
  password: form.password,
});

import React from 'react';
import { useForm } from 'react-hook-form';
import { RegisterFormData, registerSchema } from '../../utils';
import { zodResolver } from '@hookform/resolvers/zod';

interface RegisterProps {
  onSubmit: (userData: RegisterFormData) => Promise<void>;
}

const Register: React.FC<RegisterProps> = ({ onSubmit }) => {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<RegisterFormData>({
    resolver: zodResolver(registerSchema),
  });

  return (
    <div>
      <h2>Register</h2>
      <form onSubmit={handleSubmit(onSubmit)}>
        <input {...register('username')} placeholder='Username' />
        <p>{errors.username?.message}</p>

        <input {...register('email')} placeholder='Email' type='email' />
        <p>{errors.email?.message}</p>

        <input
          {...register('password')}
          placeholder='Password'
          type='password'
        />
        <p>{errors.password?.message}</p>

        <input
          {...register('passwordConfirm')}
          placeholder='Confirm Password'
          type='password'
        />
        <p>{errors.passwordConfirm?.message}</p>

        <button type='submit' disabled={isSubmitting}>
          {isSubmitting ? 'Registering...' : 'Register'}
        </button>
      </form>

      <div>
        <p>
          Already have an account?{' '}
          <span>
            <a href='/login'>Login</a>
          </span>
        </p>
      </div>
    </div>
  );
};

export default Register;

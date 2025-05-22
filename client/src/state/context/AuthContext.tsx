import { createContext, useContext, useState } from 'react';
import { loginService, registerService } from '../../services/authService';
import { NotificationData, User, JwtResponse, RegisterRequest } from '../../types';
import { LoginFormData } from '../../utils';

interface AuthContextProps {
  user: User | null;
  token: string | null;
  login: (data: LoginFormData) => Promise<NotificationData>;
  register: (data: RegisterRequest) => Promise<NotificationData>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(
    localStorage.getItem('token'),
  );

  const login = async (userData: LoginFormData): Promise<NotificationData> => {
    try {
      const data = await loginService(userData);
      authenticate(data);
      return { type: 'success', message: 'Login successful!' };
    } catch (error) {
      console.error('Login failed:', error);
      return {
        type: 'error',
        message: 'Invalid credentials. Please try again.',
      };
    }
  };

  const register = async (
    userData: RegisterRequest,
  ): Promise<NotificationData> => {
    try {
      const data = await registerService(userData);
      authenticate(data);
      return { type: 'success', message: 'Register successful!' };
    } catch (error) {
      console.error('Register failed:', error);
      return {
        type: 'error',
        message: 'Invalid credentials. Please try again.',
      };
    }
  };

  const logout = async () => {
    localStorage.removeItem('token');
    localStorage.removeItem('token_expiry');
    localStorage.removeItem('user');
    setUser(null);
    setToken(null);
  };

  const authenticate = async (data: JwtResponse) => {
    localStorage.setItem('token', data.token);
    localStorage.setItem(
      'token_expiry',
      String(Date.now() + data.expiresIn * 60 * 1000),
    );
    localStorage.setItem('user', JSON.stringify(data.user));

    setToken(data.token);
    setUser(data.user);
  };

  return (
    <AuthContext.Provider value={{ user, token, login, register, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth must be used within an AuthProvider');
  return context;
};

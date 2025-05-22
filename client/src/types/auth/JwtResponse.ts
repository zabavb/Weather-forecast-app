import { SetStateAction } from 'react';
import { User } from './User';

export interface JwtResponse {
  token: string;
  expiresIn: number;
  user: SetStateAction<User | null>;
}

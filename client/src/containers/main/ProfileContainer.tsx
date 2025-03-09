import { useNavigate } from 'react-router-dom';
import Profile from '../../components/main/Profile';
import { useAuth } from '../../state/context';
import { User } from '../../types';

const ProfileContainer: React.FC = () => {
  const user: User = localStorage.getItem('user')
    ? JSON.parse(localStorage.getItem('user') as string)
    : null;
  const navigate = useNavigate();
  const { logout } = useAuth();

  return <Profile data={user} onNavigate={navigate} onLogout={logout} />;
};

export default ProfileContainer;

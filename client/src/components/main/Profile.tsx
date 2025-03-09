import { User } from '../../types';

interface ProfileProps {
  data: User;
  onLogout: () => void;
  onNavigate: (route: string) => void;
}

const Profile: React.FC<ProfileProps> = ({ data, onNavigate, onLogout }) => {
  return (
    <>
      <button onClick={() => onNavigate('/')}>← Back</button>
      <button onClick={() => onLogout()}>Logout</button>
      <h1>My Profile</h1>
      <hr />
      <h3>
        Username: {data.username}
        <span>
          <h5>Id: {data.id}</h5>
        </span>
      </h3>
      <h4>Email: {data.email}</h4>
    </>
  );
};

export default Profile;

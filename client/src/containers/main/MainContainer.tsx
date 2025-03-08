import { useNavigate } from "react-router-dom";
import Main from "../../components/main/Main";

const MainContainer: React.FC = () => {
  const isAuth = localStorage.getItem("token") ? true : false
  const navigate = useNavigate();
  const handleNavigate = (route: string) => navigate(route)

  return <Main isAuthenticated={isAuth} onNavigate={handleNavigate} />
}

export default MainContainer;
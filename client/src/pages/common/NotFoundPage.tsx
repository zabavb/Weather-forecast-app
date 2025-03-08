import { useNavigate } from "react-router-dom"

const NotFoundPage: React.FC = () => {
  const navigate = useNavigate()
  return (
    <>
      <h1>404 Page Not Found</h1>
      <button onClick={() => navigate("/")}>Go to main page</button>
    </>
  )
}

export default NotFoundPage
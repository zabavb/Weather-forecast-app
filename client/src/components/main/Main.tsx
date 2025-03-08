interface MainProps {
  isAuthenticated: boolean
  onNavigate: (path: string) => void
}

const Main: React.FC<MainProps> = ({ isAuthenticated, onNavigate }) => {
  return (
    <>
      {isAuthenticated ? <></> : <button onClick={() => onNavigate("/login")}>Login</button>}
      <button onClick={() => onNavigate("/profile")}>My Profile</button>
      <h1>Main Page</h1>
    </>
  )
}

export default Main
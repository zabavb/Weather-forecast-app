import { Route, Routes } from "react-router-dom"
import { BrowserRouter } from "react-router-dom"

import LoginPage from "./pages/auth/LoginPage"
import RegisterPage from "./pages/auth/RegisterPage"

import MainPage from "./pages/main/MainPage"

import { AuthProvider } from "./state/context/AuthContext"
import PrivateRoute from "./privateRoute"

import NotFoundPage from "./pages/common/NotFoundPage"
import ForbiddenPage from "./pages/common/ForbiddenPage"
import ProfilePage from "./pages/main/ProfilePage"

const AppRoutes = () => (
  <AuthProvider>
    <BrowserRouter>
      <Routes>
        {/* Authentication */}
        <Route
          path="/login"
          element={<LoginPage />}
        />
        <Route
          path="/register"
          element={<RegisterPage />}
        />
        {/* Main */}
        <Route
          path="/"
          element={<MainPage />}
        />
        {/* Authenticated access for profile */}
        <Route element={<PrivateRoute />}>
          <Route
            path="/profile"
            element={<ProfilePage />}
          />
        </Route>

        {/* Other */}
        <Route
          path="*"
          element={<NotFoundPage />}
        />
        <Route
          path="/forbidden"
          element={<ForbiddenPage />}
        />
      </Routes>
    </BrowserRouter>
  </AuthProvider>

)

export default AppRoutes

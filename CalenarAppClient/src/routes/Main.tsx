import { Route, Routes } from 'react-router-dom'
import { Home } from './Home/Home'
import { LoginPage } from './Authorization/LoginPage'
import { RegisterPage } from './Authorization/RegisterPage'

export function MainRoutes() {
  return (
    <Routes>
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/" element={<Home />} />
    </Routes>
  )
}

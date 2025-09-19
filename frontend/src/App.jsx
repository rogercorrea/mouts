import React from 'react'
import { BrowserRouter, Routes, Route, Link } from 'react-router-dom'
import Login from './pages/Login'
import Employees from './pages/Employees'
import CreateEmployee from './pages/CreateEmployee'

export default function App() {
  return (
    <BrowserRouter>
      <nav style={{ padding: 10 }}>
        <Link to="/">Home</Link> | <Link to="/employees">Employees</Link> | <Link to="/create">Create</Link>
      </nav>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/employees" element={<Employees />} />
        <Route path="/create" element={<CreateEmployee />} />
      </Routes>
    </BrowserRouter>
  )
}

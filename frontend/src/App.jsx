import React, { useEffect } from "react";
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";
import Login from "./pages/Login";
import Employees from "./pages/Employees";
import CreateEmployee from "./pages/CreateEmployee";

import { NotificationsProvider, useNotifications } from "./context/NotificationsContext";
import { setNotifier } from "./utils/notifications";

function AppContent() {
  const notifications = useNotifications();

  // Inicializa o notifications.js com os métodos do contexto
  useEffect(() => {
    setNotifier(notifications);
  }, [notifications]);

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
  );
}

export default function App() {
  return (
    <NotificationsProvider>
      <AppContent />
    </NotificationsProvider>
  );
}

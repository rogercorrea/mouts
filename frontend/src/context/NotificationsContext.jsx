// NotificationsContext.jsx
import React, { createContext, useContext, useState } from "react";

const NotificationsContext = createContext();

export function useNotifications() {
  return useContext(NotificationsContext);
}

export function NotificationsProvider({ children }) {
  const [notifications, setNotifications] = useState([]);

  const addNotification = (type, message, duration = 3000) => {
    const id = Date.now();
    setNotifications((prev) => [...prev, { id, type, message }]);
    setTimeout(() => {
      setNotifications((prev) => prev.filter((n) => n.id !== id));
    }, duration);
  };

  const notifySuccess = (message, duration) => addNotification("success", message, duration);
  const notifyError = (message, duration) => addNotification("error", message, duration);

  return (
    <NotificationsContext.Provider value={{ notifySuccess, notifyError }}>
      {children}
      <div style={{ position: "fixed", top: 20, right: 20, zIndex: 9999 }}>
        {notifications.map((n) => (
          <div
            key={n.id}
            style={{
              marginBottom: 10,
              padding: "10px 20px",
              borderRadius: 5,
              color: "#fff",
              backgroundColor: n.type === "success" ? "green" : "red",
              boxShadow: "0px 2px 6px rgba(0,0,0,0.2)",
            }}
          >
            {n.message}
          </div>
        ))}
      </div>
    </NotificationsContext.Provider>
  );
}

// src/api/auth.js
import request from "./client";

export async function login(email, password) {
  return request("/api/auth/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ Email: email, Password: password }),
  });
}

export async function logout() {
  return request("/api/auth/logout", { method: "POST" });
}

export async function getProfile() {
  return request("/api/auth/profile");
}

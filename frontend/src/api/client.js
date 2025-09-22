// src/api/client.js

import { handleResponse } from "../utils/notifications";

export async function request(url, options = {}) {
  const baseUrl = process.env.REACT_APP_API_URL || "http://localhost:8080";
  const response = await fetch(baseUrl + url, {
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {}),
    },
    ...options,
  });

  return handleResponse(response);
}

export default request;

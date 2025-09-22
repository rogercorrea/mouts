// src/api/auth.js
import request from "./client";

export async function listAll() {
    const token = localStorage.getItem('token')
    return request("/api/employees", {
        headers: {
            "Content-Type": "application/json",
            Authorization: 'Bearer ' + token
        }
    });
}

export async function create(fields) {
    const token = localStorage.getItem('token')
    return request("/api/employees", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: 'Bearer ' + token
        },
        body: fields,
    });
}
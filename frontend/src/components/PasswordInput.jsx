// src/components/PasswordInput.jsx
import React, { useState } from "react";
import { validatePassword, MIN_LENGTH } from "../utils/validatePassword";

export default function PasswordInput({
  value,
  onChange,
  id = "password",
  label = "Password",
  showRequirements = true,
  width = "100%",
  height = "auto",
}) {
  const [visible, setVisible] = useState(false);
  const result = validatePassword(value ?? "");

  const getColor = (score) => {
    if (score <= 2) return "#e74c3c"; // red
    if (score <= 4) return "#f1c40f"; // yellow
    return "#2ecc71"; // green
  };

  return (
    <div style={{ marginBottom: 12 }}>
      <label htmlFor={id} style={{ display: "block", marginBottom: 6 }}>
        {label}
      </label>

      <div style={{ display: "flex", alignItems: "center", width, height }}>
        <input
          id={id}
          type={visible ? "text" : "password"}
          value={value}
          onChange={(e) => onChange?.(e.target.value)}
          aria-describedby={`${id}-requirements`}
        />
        <button
          type="button"
          onClick={() => setVisible((v) => !v)}
          aria-label={visible ? "Hide password" : "Show password"}
          style={{
            cursor: "pointer",
            borderRadius: 4,
            border: "1px solid #ccc",
            background: "#fff",
          }}
        >
          {visible ? "Hide" : "Show"}
        </button>
      </div>

      {/* Strength bar */}
      <div
        aria-hidden
        style={{
          height: 8,
          background: "#eee",
          borderRadius: 4,
          marginTop: 8,
          width,
        }}
      >
        <div
          style={{
            width: `${(result.score / 6) * 100}%`,
            height: "100%",
            background: getColor(result.score),
            borderRadius: 4,
            transition: "width 150ms ease",
          }}
        />
      </div>

      {/* Requirements */}
      {showRequirements && (
        <ul
          id={`${id}-requirements`}
          style={{
            marginTop: 8,
            paddingLeft: 18,
            color: "#333",
            lineHeight: "1.4",
          }}
        >
          <li style={{ color: result.length ? "#2ecc71" : "#666" }}>
            Minimum {MIN_LENGTH} characters
          </li>
          <li style={{ color: result.lowercase ? "#2ecc71" : "#666" }}>
            At least one lowercase letter
          </li>
          <li style={{ color: result.uppercase ? "#2ecc71" : "#666" }}>
            At least one uppercase letter
          </li>
          <li style={{ color: result.number ? "#2ecc71" : "#666" }}>
            At least one number
          </li>
          <li style={{ color: result.special ? "#2ecc71" : "#666" }}>
            At least one special character (e.g. !@#$%)
          </li>
          <li style={{ color: result.noSpaces ? "#2ecc71" : "#666" }}>
            No spaces
          </li>
        </ul>
      )}
    </div>
  );
}

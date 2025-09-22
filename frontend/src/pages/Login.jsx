import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import {login} from '../api/auth'

export default function Login() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const nav = useNavigate()

  async function submit(e) {
    e.preventDefault()
    const res = await login(email, password);
    if (res.token) {
      localStorage.setItem('token', res.token.trim())
      nav('/employees')
    } else {
      alert(res.message || 'Login failed')
    }
  }

  return (
    <div style={{ padding: 20 }}>
      <h2>Login</h2>
      <form onSubmit={submit}>
        <div>
          <label>Email</label><br/>
          <input value={email} onChange={e => setEmail(e.target.value)} />
        </div>
        <div>
          <label>Password</label><br/>
          <input type="password" value={password} onChange={e => setPassword(e.target.value)} />
        </div>
        <button type="submit">Login</button>
      </form>
    </div>
  )
}

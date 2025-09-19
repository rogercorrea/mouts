import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'

export default function CreateEmployee() {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [doc, setDoc] = useState('')
  const [birthDate, setBirthDate] = useState('')
  const [password, setPassword] = useState('')
  const [role, setRole] = useState('User')
  const [managerId, setManagerId] = useState('')
  const nav = useNavigate()

  async function submit(e) {
    e.preventDefault()
    const token = localStorage.getItem('token')
    const body = {
      firstName, lastName, email,
      documentNumber: doc,
      birthDate,
      password,
      role,
      managerId: managerId || null
    }
    const res = await fetch('/api/employees', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json', Authorization: 'Bearer ' + token },
      body: JSON.stringify(body)
    })
    const data = await res.json()
    if (res.ok) {
      alert('Created')
      nav('/employees')
    } else {
      alert(data.message || 'Error creating')
    }
  }

  return (
    <div style={{ padding: 20 }}>
      <h2>Create Employee</h2>
      <form onSubmit={submit}>
        <div><label>First Name</label><br/><input value={firstName} onChange={e => setFirstName(e.target.value)} required/></div>
        <div><label>Last Name</label><br/><input value={lastName} onChange={e => setLastName(e.target.value)} required/></div>
        <div><label>Email</label><br/><input value={email} onChange={e => setEmail(e.target.value)} type='email' required/></div>
        <div><label>Document Number</label><br/><input value={doc} onChange={e => setDoc(e.target.value)} required/></div>
        <div><label>Birth Date</label><br/><input value={birthDate} onChange={e => setBirthDate(e.target.value)} type='date' required/></div>
        <div><label>Password</label><br/><input value={password} onChange={e => setPassword(e.target.value)} type='password' required/></div>
        <div><label>Role</label><br/>
          <select value={role} onChange={e => setRole(e.target.value)}>
            <option>User</option><option>Leader</option><option>Director</option><option>Admin</option>
          </select>
        </div>
        <div><label>Manager Id (optional)</label><br/><input value={managerId} onChange={e => setManagerId(e.target.value)} /></div>
        <button type='submit'>Create</button>
      </form>
    </div>
  )
}

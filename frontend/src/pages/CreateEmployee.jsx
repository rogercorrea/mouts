import React, { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import { create } from '../api/employees'
import { validatePassword } from '../utils/validatePassword';
import PasswordInput from '../components/PasswordInput';
import { notifyError, notifySuccess } from '../utils/notifications';

export default function CreateEmployee() {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [doc, setDoc] = useState('')
  const [birthDate, setBirthDate] = useState('')
  const [password, setPassword] = useState('')
  const [role, setRole] = useState(0)
  const [managerId, setManagerId] = useState('')
  const nav = useNavigate()

  async function submit(e) {
    e.preventDefault();

    const check = validatePassword(password);
    if (!check.valid) {
      notifyError("Weak password — please fix the requirements before continuing.");
      return;
    }

    const body = {
      firstName, lastName, email,
      documentNumber: doc,
      birthDate,
      password,
      Role: Number(role),
      managerId: managerId || null
    }
    const res = await create(JSON.stringify(body));
    if (res.id) {
      notifySuccess('Created employee successfully');
      nav('/employees')
    } else {
      notifyError(res || 'Error creating')
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
        <div><PasswordInput value={password} onChange={setPassword} id="register-password" label="password" width="80px" height="16px" required /></div>
        <div><label>Role</label><br/>
          <select value={role} onChange={e => setRole(e.target.value)}>
            <option value={0}>User</option><option value={1}>Leader</option><option value={2}>Director</option><option value={3}>Admin</option>
          </select>
        </div>
        <div><label>Manager Id (optional)</label><br/><input value={managerId} onChange={e => setManagerId(e.target.value)} /></div>
        <button type='submit'>Create</button>
      </form>
    </div>
  )
}

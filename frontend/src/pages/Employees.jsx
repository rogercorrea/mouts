import React, { useEffect, useState } from 'react'

export default function Employees() {
  const [list, setList] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    (async () => {
      const token = localStorage.getItem('token')
      const res = await fetch('/api/employees', {
        headers: { Authorization: 'Bearer ' + token }
      })
      if (res.ok) {
        const data = await res.json()
        setList(data)
      } else {
        const err = await res.json()
        alert(err.message || 'Failed to load')
      }
      setLoading(false)
    })()
  }, [])

  if (loading) return <div>Loading...</div>

  return (
    <div style={{ padding: 20 }}>
      <h2>Employees</h2>
      <table border="1" cellPadding="8">
        <thead><tr><th>Name</th><th>Email</th><th>Role</th></tr></thead>
        <tbody>
          {list.map(e => (
            <tr key={e.id}>
              <td>{e.firstName} {e.lastName}</td>
              <td>{e.email}</td>
              <td>{e.role}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

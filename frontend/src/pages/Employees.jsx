import React, { useEffect, useState } from 'react'
import {listAll} from '../api/employees'

export default function Employees() {
  const [list, setList] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    (async () => {
      const res = await listAll();
      console.log(res);
      if (res) {
        setList(res);
      } else {
        const err = res;
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

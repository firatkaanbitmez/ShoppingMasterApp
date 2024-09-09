import React, { useEffect, useState } from 'react';
import { getUsers, createUser, updateUser, deleteUser } from '../services/userService';

const UserPage = () => {
  const [users, setUsers] = useState([]);
  const [newUser, setNewUser] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
    roles: 'Admin',  // Varsayılan rol Admin
    address: {
      addressLine1: '',
      addressLine2: '',
      city: '',
      state: '',
      postalCode: '',  
      country: ''
    }
  });
  const [editingUser, setEditingUser] = useState(null);
  const [errorMessage, setErrorMessage] = useState(null);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getUsers();
      setUsers(data);  // API yanıtındaki 'data' array'ini kullanıyoruz
    } catch (error) {
      console.error('Error fetching users:', error);
    }
  };

  const handleCreateUser = async () => {
    // Basit doğrulama
    if (!newUser.firstName || !newUser.email || !newUser.password || !newUser.address.postalCode) {
      setErrorMessage("Please fill in all required fields.");
      return;
    }

    try {
      await createUser(newUser);
      setNewUser({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        roles: 'Admin',
        address: {
          addressLine1: '',
          addressLine2: '',
          city: '',
          state: '',
          postalCode: '',
          country: ''
        }
      });
      setErrorMessage(null);
      fetchUsers();  // Kullanıcıyı ekledikten sonra kullanıcıları güncelle
    } catch (error) {
      setErrorMessage("Error creating user, please try again.");
      console.error('Error creating user:', error);
    }
  };

  return (
    <div className="user-management">
      <h1>User Management</h1>

      <div className="user-form">
        <h2>Add New User</h2>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
        <form>
          <label>First Name</label>
          <input
            type="text"
            placeholder="First Name"
            value={newUser.firstName}
            onChange={(e) => setNewUser({ ...newUser, firstName: e.target.value })}
          />
          <label>Last Name</label>
          <input
            type="text"
            placeholder="Last Name"
            value={newUser.lastName}
            onChange={(e) => setNewUser({ ...newUser, lastName: e.target.value })}
          />
          <label>Email</label>
          <input
            type="email"
            placeholder="Email"
            value={newUser.email}
            onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
          />
          <label>Password</label>
          <input
            type="password"
            placeholder="Password"
            value={newUser.password}
            onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
          />
          <label>Address Line 1</label>
          <input
            type="text"
            placeholder="Address Line 1"
            value={newUser.address.addressLine1}
            onChange={(e) => setNewUser({
              ...newUser,
              address: { ...newUser.address, addressLine1: e.target.value }
            })}
          />
          <label>Postal Code</label>
          <input
            type="text"
            placeholder="Postal Code"
            value={newUser.address.postalCode}
            onChange={(e) => setNewUser({
                ...newUser,
                address: { ...newUser.address, postalCode: e.target.value }
            })}
          />
          <label>City</label>
          <input
            type="text"
            placeholder="City"
            value={newUser.address.city}
            onChange={(e) => setNewUser({
              ...newUser,
              address: { ...newUser.address, city: e.target.value }
            })}
          />
          <label>Country</label>
          <input
            type="text"
            placeholder="Country"
            value={newUser.address.country}
            onChange={(e) => setNewUser({
              ...newUser,
              address: { ...newUser.address, country: e.target.value }
            })}
          />
          <button type="button" onClick={handleCreateUser}>Add User</button>
        </form>
      </div>

      <h2>Users</h2>
      <ul className="user-list">
        {users.length > 0 ? (
          users.map((user) => (
            <li key={user.id}>
              {user.firstName} {user.lastName} - {user.email}
            </li>
          ))
        ) : (
          <p>No users found.</p>
        )}
      </ul>
    </div>
  );
};

export default UserPage;

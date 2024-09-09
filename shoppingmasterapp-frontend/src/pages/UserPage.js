import React, { useEffect, useState } from 'react';
import { getUsers, createUser, updateUser, deleteUser } from '../services/userService';

const UserPage = () => {
  const [users, setUsers] = useState([]);
  const [newUser, setNewUser] = useState({
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
  const [editingUser, setEditingUser] = useState(null);
  const [errorMessage, setErrorMessage] = useState(null);
  const [showPopup, setShowPopup] = useState(false);
  const [isCreating, setIsCreating] = useState(false);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const data = await getUsers();
      setUsers(data);
    } catch (error) {
      console.error('Error fetching users:', error);
    }
  };

  const handleCreateUser = async () => {
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
      setShowPopup(false);
      fetchUsers();
    } catch (error) {
      setErrorMessage("Error creating user, please try again.");
      console.error('Error creating user:', error);
    }
  };

  const handleDeleteUser = async (userId) => {
    if (!userId) {
      console.error("User ID is undefined");
      return;
    }

    const confirmDelete = window.confirm('Are you sure you want to delete this user?');
    if (confirmDelete) {
      try {
        await deleteUser(userId);
        fetchUsers();
      } catch (error) {
        console.error('Error deleting user:', error);
      }
    }
  };

  const handleUpdateUser = (user) => {
    if (!user || !user.id) {
      console.error("Selected user or user ID is missing");
      return;
    }

    setEditingUser(user);
    setShowPopup(true);
    setIsCreating(false);
  };

  const handleSaveUpdatedUser = async () => {
    if (!editingUser || !editingUser.id) {
      console.error("Editing user or user ID is missing");
      return;
    }

    try {
      await updateUser(editingUser);
      setEditingUser(null);
      setShowPopup(false);
      fetchUsers();
    } catch (error) {
      console.error('Error updating user:', error);
    }
  };

  return (
    <div className="user-management">
      <h1>User Management</h1>
      <button className="create-user-btn" onClick={() => { setShowPopup(true); setIsCreating(true); }}>Create User</button>

      <h2>Users</h2>
      <table className="user-list-table">
        <thead>
          <tr>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Roles</th>
            <th>Address Line 1</th>
            <th>Postal Code</th>
            <th>City</th>
            <th>Country</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {users.length > 0 ? (
            users.map((user) => (
              <tr key={user.id}>
                <td>{user.firstName}</td>
                <td>{user.lastName}</td>
                <td>{user.email}</td>
                <td>{user.roles}</td>
                <td>{user.address.addressLine1}</td>
                <td>{user.address.postalCode}</td>
                <td>{user.address.city}</td>
                <td>{user.address.country}</td>
                <td className="action-buttons">
                  <button className="update-btn" onClick={() => handleUpdateUser(user)}>Update</button>
                  <button className="delete-btn" onClick={() => handleDeleteUser(user.id)}>Delete</button>
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="9">No users found.</td>
            </tr>
          )}
        </tbody>
      </table>

      {showPopup && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>{isCreating ? 'Create User' : 'Update User'}</h2>
            <form className="create-user-form">
              <label>First Name</label>
              <input
                type="text"
                value={isCreating ? newUser.firstName : editingUser.firstName}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({ ...newUser, firstName: e.target.value })
                    : setEditingUser({ ...editingUser, firstName: e.target.value })
                }
              />
              <label>Last Name</label>
              <input
                type="text"
                value={isCreating ? newUser.lastName : editingUser.lastName}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({ ...newUser, lastName: e.target.value })
                    : setEditingUser({ ...editingUser, lastName: e.target.value })
                }
              />
              <label>Email</label>
              <input
                type="email"
                value={isCreating ? newUser.email : editingUser.email}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({ ...newUser, email: e.target.value })
                    : setEditingUser({ ...editingUser, email: e.target.value })
                }
              />
              <label>Password</label>
              <input
                type="password"
                value={isCreating ? newUser.password : editingUser.password}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({ ...newUser, password: e.target.value })
                    : setEditingUser({ ...editingUser, password: e.target.value })
                }
              />
              <label>Roles</label>
              <select
                value={isCreating ? newUser.roles : editingUser.roles}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({ ...newUser, roles: e.target.value })
                    : setEditingUser({ ...editingUser, roles: e.target.value })
                }
              >
                <option value="Admin">Admin</option>
                <option value="Customer">Customer</option>
              </select>
              <label>Address Line 1</label>
              <input
                type="text"
                value={isCreating ? newUser.address.addressLine1 : editingUser.address.addressLine1}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({
                        ...newUser,
                        address: { ...newUser.address, addressLine1: e.target.value }
                      })
                    : setEditingUser({
                        ...editingUser,
                        address: { ...editingUser.address, addressLine1: e.target.value }
                      })
                }
              />
              <label>Postal Code</label>
              <input
                type="text"
                value={isCreating ? newUser.address.postalCode : editingUser.address.postalCode}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({
                        ...newUser,
                        address: { ...newUser.address, postalCode: e.target.value }
                      })
                    : setEditingUser({
                        ...editingUser,
                        address: { ...editingUser.address, postalCode: e.target.value }
                      })
                }
              />
              <label>City</label>
              <input
                type="text"
                value={isCreating ? newUser.address.city : editingUser.address.city}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({
                        ...newUser,
                        address: { ...newUser.address, city: e.target.value }
                      })
                    : setEditingUser({
                        ...editingUser,
                        address: { ...editingUser.address, city: e.target.value }
                      })
                }
              />
              <label>Country</label>
              <input
                type="text"
                value={isCreating ? newUser.address.country : editingUser.address.country}
                onChange={(e) =>
                  isCreating
                    ? setNewUser({
                        ...newUser,
                        address: { ...newUser.address, country: e.target.value }
                      })
                    : setEditingUser({
                        ...editingUser,
                        address: { ...editingUser.address, country: e.target.value }
                      })
                }
              />
              <div className="modal-buttons">
                {isCreating ? (
                  <button type="button" onClick={handleCreateUser}>
                    Create
                  </button>
                ) : (
                  <button type="button" onClick={handleSaveUpdatedUser}>
                    Save
                  </button>
                )}
                <button type="button" onClick={() => setShowPopup(false)}>
                  Cancel
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default UserPage;

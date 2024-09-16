import React, { useState, useEffect } from 'react';
import { apiService } from '../services/apiService';

const ProfilePage = () => {
  const [user, setUser] = useState({
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  });
  const [error, setError] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  useEffect(() => {
    const fetchUserData = async () => {
      const token = localStorage.getItem('token');
      if (!token) {
        setError('No token found. Please login.');
        return;
      }
      try {
        const response = await apiService.getUserData();
        if (response) {
          setUser(response.data);
        } else {
          setError('User data could not be retrieved.');
        }
      } catch (error) {
        setError('Failed to load user data');
      }
    };
    fetchUserData();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUser((prevUser) => ({ ...prevUser, [name]: value || '' }));  // Ensure empty string if undefined
  };

  const handleUpdateProfile = async (e) => {
    e.preventDefault();
    setError('');
    setSuccessMessage('');
    try {
      if (user.firstName && user.lastName && user.email) {  // Ensure mandatory fields are present
        await apiService.updateCustomer(user);
        setSuccessMessage('Profile updated successfully!');
      } else {
        setError('All fields are required.');
      }
    } catch (error) {
      setError('Error updating profile');
    }
  };

  const handlePasswordChange = async (e) => {
    e.preventDefault();
    if (user.password) {
      try {
        await apiService.updateCustomerPassword(user.id, user.password);
        setSuccessMessage('Password updated successfully!');
      } catch (error) {
        setError('Error updating password');
      }
    } else {
      setError('Password cannot be empty');
    }
  };

  return (
    <div className="profile-page">
      <h2>Update Profile</h2>
      {error && <p className="error-message">{error}</p>}
      {successMessage && <p className="success-message">{successMessage}</p>}
      <form onSubmit={handleUpdateProfile}>
        <input
          type="text"
          name="firstName"
          placeholder="First Name"
          value={user.firstName || ''}  // Ensure it's always a string
          onChange={handleInputChange}
          required
        />
        <input
          type="text"
          name="lastName"
          placeholder="Last Name"
          value={user.lastName || ''}  // Ensure it's always a string
          onChange={handleInputChange}
          required
        />
        <input
          type="email"
          name="email"
          placeholder="Email"
          value={user.email || ''}  // Ensure it's always a string
          onChange={handleInputChange}
          required
          disabled  // Assuming email cannot be changed
        />
        <button type="submit">Update Profile</button>
      </form>

      <h3>Change Password</h3>
      <form onSubmit={handlePasswordChange}>
        <input
          type="password"
          name="password"
          placeholder="New Password"
          value={user.password || ''}  // Ensure it's always a string
          onChange={handleInputChange}
        />
        <button type="submit">Change Password</button>
      </form>
    </div>
  );
};

export default ProfilePage;

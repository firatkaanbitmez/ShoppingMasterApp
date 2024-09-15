import React, { useState } from 'react';
import { register } from '../services/apiService';
import { useNavigate } from 'react-router-dom';
import '../assets/registerPage.css'; // CSS dosyası

const RegisterPage = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [address, setAddress] = useState({
    addressLine1: '',
    addressLine2: '',
    city: '',
    state: '',
    postalCode: '',
    country: '',
  });
  const [error, setError] = useState('');
  const [successMessage, setSuccessMessage] = useState('');
  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setAddress((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleRegister = async (e) => {
    e.preventDefault();
    setError('');
    setSuccessMessage('');

    // Tüm alanların dolu olup olmadığını kontrol edin
    if (firstName && lastName && email && password && address.addressLine1 && address.city && address.state && address.postalCode && address.country) {
      try {
        const response = await register({
          firstName,
          lastName,
          email,
          password,
          address
        });
        setSuccessMessage('Registration successful! You can now login.');
      } catch (error) {
        setError('Registration failed. Please try again.');
      }
    } else {
      setError('All fields are required. Please fill them correctly.');
    }
  };

  const handleNavigateToLogin = () => {
    navigate('/login');
  };

  return (
    <div className="register-page">
      <h2>Register</h2>
      {error && <p className="error-message">{error}</p>}
      {successMessage ? (
        <div className="success-message-box">
          <p>{successMessage}</p>
          <button onClick={handleNavigateToLogin} className="login-btn">
            Go to Login
          </button>
        </div>
      ) : (
        <form onSubmit={handleRegister}>
          <input
            type="text"
            placeholder="First Name"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />
          <input
            type="text"
            placeholder="Last Name"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <h3>Address Information</h3>
          <input
            type="text"
            name="addressLine1"
            placeholder="Address Line 1"
            value={address.addressLine1}
            onChange={handleInputChange}
            required
          />
          <input
            type="text"
            name="addressLine2"
            placeholder="Address Line 2"
            value={address.addressLine2}
            onChange={handleInputChange}
          />
          <input
            type="text"
            name="city"
            placeholder="City"
            value={address.city}
            onChange={handleInputChange}
            required
          />
          <input
            type="text"
            name="state"
            placeholder="State"
            value={address.state}
            onChange={handleInputChange}
            required
          />
          <input
            type="text"
            name="postalCode"
            placeholder="Postal Code"
            value={address.postalCode}
            onChange={handleInputChange}
            required
          />
          <input
            type="text"
            name="country"
            placeholder="Country"
            value={address.country}
            onChange={handleInputChange}
            required
          />

          <button type="submit" className="register-btn">Register</button>
        </form>
      )}
    </div>
  );
};

export default RegisterPage;

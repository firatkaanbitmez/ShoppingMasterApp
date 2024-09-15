import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import '../assets/header.css';

const Header = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('token');
    setIsLoggedIn(false);
    navigate('/login');
  };

  return (
    <header className="header">
      <div className="header-top">
        <LogoSection />
        <SearchBar />
        <AccountCartSection isLoggedIn={isLoggedIn} handleLogout={handleLogout} />
      </div>
      <CategoryMenu />
    </header>
  );
};

const LogoSection = () => (
  <div className="logo-section">
    <Link to="/" className="logo-link">
      <img src="/shoppingmaster-icon.png" alt="Shopping Master Logo" className="logo-img" />
      <h1 className="logo-text">Shopping Master</h1>
    </Link>
  </div>
);

const SearchBar = () => (
  <div className="search-bar">
    <input
      type="text"
      className="search-input"
      placeholder="Search products, categories..."
      aria-label="Search products and categories"
    />
    <button className="search-button">
      <FontAwesomeIcon icon={faSearch} />
    </button>
  </div>
);

const AccountCartSection = ({ isLoggedIn, handleLogout }) => (
  <div className="account-cart-section">
    {isLoggedIn ? (
      <div className="dropdown">
        <span className="account-link">Account</span>
        <div className="dropdown-content">
          <Link to="/profile">Profile</Link>
          <Link to="/settings">Settings</Link>
          <button className="logout-button" onClick={handleLogout}>Logout</button>
        </div>
      </div>
    ) : (
      <div className="auth-links">
        <Link to="/login" className="login-link">Login</Link>
        <Link to="/register" className="signup-link">Sign Up</Link>
      </div>
    )}
    <Link to="/cart" className="cart-link">
      <img src="/cart.png" alt="Shopping Cart" className="cart-image" />
      <span id="cartItemCount" className="badge">0</span>
    </Link>
  </div>
);

const CategoryMenu = () => (
  <nav className="category-menu">
    <ul>
      <li><Link to="/customers">Customers</Link></li>
      <li><Link to="/order">Order</Link></li>
      <li><Link to="/cart">Cart</Link></li>
      <li><Link to="/category">Category</Link></li>
      <li><Link to="/discount">Discount</Link></li>
      <li><Link to="/payment">Payment</Link></li>
      <li><Link to="/shipping">Shipping</Link></li>
    </ul>
  </nav>
);

export default Header;

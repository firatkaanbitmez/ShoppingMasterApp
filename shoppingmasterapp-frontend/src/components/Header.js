import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch } from '@fortawesome/free-solid-svg-icons';
import '../assets/header.css';

const Header = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const userLoggedIn = localStorage.getItem('isLoggedIn');
    if (!userLoggedIn) {
      setIsLoggedIn(true);
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem('isLoggedIn');
    setIsLoggedIn(false);
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
      <>
        <div className="dropdown">
          <Link to="/account" className="account-link">Account</Link>
          <div className="dropdown-content">
            <Link to="/profile">Profile</Link>
            <Link to="/settings">Settings</Link>
            <button className="logout-button" onClick={handleLogout}>Logout</button>
          </div>
        </div>
        <Link to="/cart" className="cart-link">
          <img src="/cart.png" alt="Shopping Cart" className="cart-image" />
          <span id="cartItemCount" className="badge">0</span>
        </Link>
      </>
    ) : (
      <>
        <Link to="/login" className="login-link">Login</Link>
        <Link to="/signup" className="signup-link">Sign Up</Link>
      </>
    )}
  </div>
);

const CategoryMenu = () => (
  <nav className="category-menu">
    <ul>
      <li><Link to="/Customers">Customers</Link></li>
      <li><Link to="/Order">Order</Link></li>
      <li><Link to="/Cart">Cart</Link></li>
      <li><Link to="/Category ">Category </Link></li>
      <li><Link to="/Discount ">Discount </Link></li>
      <li><Link to="/Payment ">Payment </Link></li>
      <li><Link to="/Shipping  ">Shipping  </Link></li>

    </ul>
  </nav>
);

export default Header;

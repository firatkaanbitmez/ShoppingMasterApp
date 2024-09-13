import React from 'react';
import { Link } from 'react-router-dom';
import '../assets/header.css';  // Header stilleri için import

const Header = () => {
  return (
    <header className="header">
      <div className="logo">
        <img src="/shoppingmaster-icon.png" alt="Shopping Master Logo" className="logo-img" /> {/* Logo için public klasöründen doğrudan src kullanılıyor */}
        <h1>Shopping Master</h1>
      </div>
      <nav>
        <ul className="nav-list">
          <li><Link to="/">Home</Link></li>
          <li><Link to="/customers">Customers</Link></li>
          <li><Link to="/cart">Cart</Link></li>
          <li><Link to="/offers">Special Offers</Link></li>
          <li><Link to="/discounts">Discounts</Link></li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;

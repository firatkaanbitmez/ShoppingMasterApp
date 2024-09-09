import React from 'react';
import { Link } from 'react-router-dom';

const Header = () => {
  return (
    <header className="header">
      <nav>
        <ul className="nav-list">
          <li><Link to="/">Home</Link></li>
          <li><Link to="/users">Users</Link></li>
          <li><Link to="/cart">Cart</Link></li>
          <li><Link to="/offers">Special Offers</Link></li>
          <li><Link to="/discounts">Discounts</Link></li>
        </ul>
      </nav>
    </header>
  );
};

export default Header;

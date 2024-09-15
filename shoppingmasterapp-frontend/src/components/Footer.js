import React from 'react';
import '../assets/footer.css';
import { FaFacebookF, FaTwitter, FaInstagram } from 'react-icons/fa';

const Footer = () => {
  return (
    <footer className="footer">
      <div className="footer-content">
        <p>&copy; 2024 Shopping Master. All rights reserved.</p>
        <div className="social-icons">
        <a href="https://www.facebook.com" className="icon" aria-label="Facebook" target="_blank" rel="noopener noreferrer">
  <FaFacebookF />
</a>
<a href="https://www.twitter.com" className="icon" aria-label="Twitter" target="_blank" rel="noopener noreferrer">
  <FaTwitter />
</a>
<a href="https://www.instagram.com" className="icon" aria-label="Instagram" target="_blank" rel="noopener noreferrer">
  <FaInstagram />
</a>

        </div>
      </div>
    </footer>
  );
};

export default Footer;

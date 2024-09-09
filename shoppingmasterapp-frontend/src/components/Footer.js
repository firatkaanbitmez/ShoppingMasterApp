import React from 'react';
import '../assets/footer.css';  // Footer stilleri için import

const Footer = () => {
  return (
    <footer className="footer">
      <p>&copy; 2024 Shopping Master. All rights reserved.</p>
      <div className="social-icons">
        <a href="#" className="icon">Facebook</a>
        <a href="#" className="icon">Twitter</a>
        <a href="#" className="icon">Instagram</a>
      </div>
    </footer>
  );
};

export default Footer;

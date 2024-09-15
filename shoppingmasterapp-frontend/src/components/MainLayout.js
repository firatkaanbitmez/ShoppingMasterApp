import React from 'react';
import Header from './Header';
import Footer from './Footer';
import '../assets/mainlayout.css';  // Stil dosyası

const MainLayout = ({ children }) => {
  return (
    <div className="main-layout">
      <Header />
      <div className="content">
        {children}
      </div>
      <Footer />
    </div>
  );
};

export default MainLayout;

import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext'; // Import AuthProvider
import HomePage from './pages/HomePage';
import CartPage from './pages/CartPage';
import ProductPage from './pages/ProductPage';
import CustomerPage from './pages/CustomerPage';
import LoginPage from './pages/LoginPage';  // Add LoginPage
import RegisterPage from './pages/RegisterPage';  // Add RegisterPage
import Header from './components/Header';
import Footer from './components/Footer';

function App() {
  return (
    <div className="App">
      <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/cart" element={<CartPage />} />
        <Route path="/product/:id" element={<ProductPage />} />
        <Route path="/Customers" element={<CustomerPage />} />
        <Route path="/login" element={<LoginPage />} />  {/* Add route */}
        <Route path="/register" element={<RegisterPage />} />  {/* Add route */}
      </Routes>
      <Footer />
    </div>
  );
}

export default App;

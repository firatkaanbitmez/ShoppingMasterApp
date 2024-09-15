import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext'; // AuthProvider import edildi
import HomePage from './pages/HomePage';
import CartPage from './pages/CartPage';
import ProductPage from './pages/ProductPage';
import CustomerPage from './pages/CustomerPage';
import LoginPage from './pages/LoginPage';  // LoginPage eklendi
import RegisterPage from './pages/RegisterPage';  // RegisterPage eklendi
import MainLayout from './components/MainLayout';  // MainLayout import edildi

function App() {
  return (
    <AuthProvider>
      <MainLayout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/product/:id" element={<ProductPage />} />
          <Route path="/customers" element={<CustomerPage />} />
          <Route path="/login" element={<LoginPage />} />  {/* Login rotası eklendi */}
          <Route path="/register" element={<RegisterPage />} />  {/* Register rotası eklendi */}
        </Routes>
      </MainLayout>
    </AuthProvider>
  );
}

export default App;

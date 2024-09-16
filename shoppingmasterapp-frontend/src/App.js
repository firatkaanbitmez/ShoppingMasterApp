import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext'; 
import HomePage from './pages/HomePage';
import CartPage from './pages/CartPage';
import ProductPage from './pages/ProductPage';
import CustomerPage from './pages/CustomerPage';
import LoginPage from './pages/LoginPage';  
import RegisterPage from './pages/RegisterPage';  
import MainLayout from './components/MainLayout';  
import ProfilePage from './pages/ProfilePage';  // Eksik importu ekledim

function App() {
  return (
    <AuthProvider>
      <MainLayout>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/cart" element={<CartPage />} />
          <Route path="/product/:id" element={<ProductPage />} />
          <Route path="/customers" element={<CustomerPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/profile" element={<ProfilePage />} />  {/* Profil sayfası rotası */}

        </Routes>
      </MainLayout>
    </AuthProvider>
  );
}

export default App;

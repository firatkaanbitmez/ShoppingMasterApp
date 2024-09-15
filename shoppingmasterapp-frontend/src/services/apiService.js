import axios from 'axios';

const API_URL = 'http://localhost:5199/api';  // API URL'nin doğruluğunu kontrol et

// Authorization token'ı header'a eklemek için fonksiyon
const authHeader = () => {
  const token = localStorage.getItem('token');
  return token ? { Authorization: `Bearer ${token}` } : {};
};

// Ürünleri getiren fonksiyon
export const getProducts = async () => {
  try {
    const response = await axios.get(`${API_URL}/product`, { headers: authHeader() });
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error.response ? error.response.data : error.message);
    throw error;
  }
};

// Sepete ürün ekleyen fonksiyon
export const addToCart = async (productId, quantity, customerId) => {
  try {
    const response = await axios.post(`${API_URL}/cart/add`, { productId, quantity, customerId }, { headers: authHeader() });
    return response.data;
  } catch (error) {
    console.error("Error adding to cart:", error.response ? error.response.data : error.message);
    throw error;
  }
};

// Sepeti getiren fonksiyon
export const getCart = async (customerId) => {
  try {
    const response = await axios.get(`${API_URL}/cart/${customerId}`, { headers: authHeader() });
    return response.data;
  } catch (error) {
    console.error("Error fetching cart:", error.response ? error.response.data : error.message);
    throw error;
  }
};

// Kullanıcı giriş fonksiyonu
export const login = async (email, password) => {
  try {
    const response = await axios.post(`${API_URL}/Customer/login`, { email, password });
    if (response.data.token) {
      localStorage.setItem('token', response.data.token);  // Token'ı kaydet
    }
    return response.data;
  } catch (error) {
    console.error("Error during login:", error.response ? error.response.data : error.message);
    throw error;
  }
};

// Kullanıcı kayıt fonksiyonu
// Kullanıcı kayıt fonksiyonu
export const register = async (customerData) => {
  try {
    const response = await axios.post(`${API_URL}/Customer/register`, customerData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error("Error during registration:", error.response ? error.response.data : error.message);
    throw error;
  }
};


// Korunan bir veri çekme fonksiyonu
export const fetchProtectedData = async () => {
  try {
    const response = await axios.get(`${API_URL}/protected-endpoint`, { headers: authHeader() });
    return response.data;
  } catch (error) {
    console.error("Error fetching protected data:", error.response ? error.response.data : error.message);
    throw error;
  }
};

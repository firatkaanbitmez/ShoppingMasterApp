import axios from 'axios';

const API_URL = 'http://localhost:5199/api';

export const getProducts = async () => {
  try {
    const response = await axios.get(`${API_URL}/product`);
    return response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
    throw error;
  }
};

export const addToCart = async (productId, quantity, customerId) => {
  try {
    const response = await axios.post(`${API_URL}/cart/add`, { productId, quantity, customerId });
    return response.data;
  } catch (error) {
    console.error("Error adding to cart:", error);
    throw error;
  }
};

export const getCart = async (customerId) => {
  try {
    const response = await axios.get(`${API_URL}/cart/${customerId}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching cart:", error);
    throw error;
  }
};

// Diğer API servisleri...

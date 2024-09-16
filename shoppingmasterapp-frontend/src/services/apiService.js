import axios from 'axios';

const API_URL = 'http://localhost:5199/api';  // Ensure correct API URL

// Authorization token handler for header
const authHeader = () => {
  const token = localStorage.getItem('token');
  console.log("Retrieved token from localStorage:", token);  // Debug log
  return token ? { Authorization: `Bearer ${token}` } : {};
};

// GET request
const getRequest = async (endpoint) => {
  try {
    const headers = authHeader();
    console.log("Request headers:", headers);  // Log headers
    const response = await axios.get(`${API_URL}/${endpoint}`, { headers });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

// POST request
const postRequest = async (endpoint, data) => {
  try {
    const response = await axios.post(`${API_URL}/${endpoint}`, data, { headers: authHeader() });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

// PUT request
const putRequest = async (endpoint, data) => {
  try {
    const response = await axios.put(`${API_URL}/${endpoint}`, data, { headers: authHeader() });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

// DELETE request
const deleteRequest = async (endpoint) => {
  try {
    const response = await axios.delete(`${API_URL}/${endpoint}`, { headers: authHeader() });
    return response.data;
  } catch (error) {
    handleError(error);
  }
};

// Error handling
const handleError = (error) => {
  console.error("API Error:", error.response ? error.response.data : error.message);
  console.log("Full error details:", error.response);  // Detailed log
  throw error;
};

// User login
const login = async (email, password) => {
  const response = await postRequest('Customer/login', { email, password });
  console.log("Login response:", response);  // Debug full response

  if (response.data) {
    localStorage.setItem('token', response.data);
    const savedToken = localStorage.getItem('token');
    console.log("Token saved and retrieved:", savedToken);

    if (!savedToken) {
      console.error("Token was not saved properly.");
    }
  } else {
    console.error("No token received during login.");
  }
  return response;
};

// API operations
export const apiService = {
  // Products
  getProducts: () => getRequest('product'),
  addToCart: (productId, quantity, customerId) => postRequest('cart/add', { productId, quantity, customerId }),
  getCart: (customerId) => getRequest(`cart/${customerId}`),

  // User operations
  login,  // Now it's properly exported and can be used
  register: (customerData) => postRequest('Customer/register', customerData),

  // Protected data operation
  fetchProtectedData: () => getRequest('protected-endpoint'),
  getUserData: () => getRequest('customer/me'),
  updateCustomerProfile: (customerData) => putRequest(`customer/${customerData.id}/update`, customerData),
  updateCustomerPassword: (customerId, newPassword) => putRequest(`customer/${customerId}/change-password`, { newPassword }),

  // Customer operations
  getCustomers: () => getRequest('customer'),
  createCustomer: (customerData) => postRequest('customer', customerData),
  updateCustomer: (customerData) => putRequest(`customer/${customerData.id}`, customerData),
  deleteCustomer: (customerId) => deleteRequest(`customer/delete/${customerId}`),
};

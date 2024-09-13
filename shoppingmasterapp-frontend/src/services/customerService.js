import axios from 'axios';

const API_URL = 'http://localhost:5199/api/customer';

export const getCustomers = async () => {
  try {
    const response = await axios.get(`${API_URL}`);
    return response.data;
  } catch (error) {
    console.error('Error fetching customers:', error);
    throw error;
  }
};

export const createCustomer = async (customerData) => {
  try {
    const response = await axios.post(`${API_URL}`, customerData);
    return response.data;
  } catch (error) {
    console.error('Error creating customer:', error);
    throw error;
  }
};

export const updateCustomer = async (customerData) => {
  try {
      const response = await axios.put(`${API_URL}/${customerData.id}`, customerData);
      return response.data;
  } catch (error) {
      console.error('Error updating customer:', error);
      throw error;
  }
};



export const deleteCustomer = async (customerId) => {
  try {
    const response = await axios.delete(`${API_URL}/delete/${customerId}`);
    return response.data;
  } catch (error) {
    console.error('Error deleting customer:', error);
    throw error;
  }
};

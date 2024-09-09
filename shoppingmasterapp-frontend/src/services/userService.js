import axios from 'axios';

const API_URL = 'http://localhost:5199/api/User';  // User endpoint'i

export const getUsers = async () => {
    try {
      const response = await axios.get('http://localhost:5199/api/User');  // Doğru endpoint
      return response.data.data;  // Eğer data kullanıcılarsa, doğrudan data array'ine ulaşalım
    } catch (error) {
      console.error("Error fetching users:", error);
      throw error;
    }
  };
  
export const createUser = async (user) => {
  try {
    const response = await axios.post(`${API_URL}/create`, user);  // Doğru endpoint
    return response.data;
  } catch (error) {
    console.error("Error creating user:", error);
    throw error;
  }
};

export const updateUser = async (id, user) => {
  try {
    const response = await axios.put(`${API_URL}/update/${id}`, user);  // Güncelleme endpoint'i kontrol edilmeli
    return response.data;
  } catch (error) {
    console.error("Error updating user:", error);
    throw error;
  }
};

export const deleteUser = async (id) => {
  try {
    const response = await axios.delete(`${API_URL}/delete/${id}`);  // Silme endpoint'i
    return response.data;
  } catch (error) {
    console.error("Error deleting user:", error);
    throw error;
  }
};

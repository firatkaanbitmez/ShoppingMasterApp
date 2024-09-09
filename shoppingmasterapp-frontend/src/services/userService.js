import axios from 'axios';

const API_URL = 'http://localhost:5199/api/User';  // User endpoint'i

export const getUsers = async () => {
  try {
    const response = await axios.get('http://localhost:5199/api/User');
    if (response.data && response.data.data) {
      return response.data.data;  // Eğer kullanıcılar response.data.data'daysa bu veriyi dönüyoruz
    } else {
      throw new Error("No users found in the response");
    }
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

export const updateUser = async (user) => {
  try {
    if (!user || !user.id) throw new Error("User or User ID is missing");
    const response = await axios.put(`${API_URL}/update`, user);  // user objesini doğru gönderiyoruz
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const deleteUser = async (userId) => {
  try {
    if (!userId) throw new Error("User ID is undefined");
    const response = await axios.delete(`${API_URL}/delete/${userId}`);
    return response.data;
  } catch (error) {
    throw error;
  }
};

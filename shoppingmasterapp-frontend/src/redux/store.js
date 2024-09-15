import { configureStore } from '@reduxjs/toolkit';
import productReducer from './productSlice';
import cartReducer from './cartSlice'; // Import the cart reducer

const store = configureStore({
  reducer: {
    products: productReducer, // Existing product reducer
    cart: cartReducer, // Add cart reducer here
  },
});

export default store;

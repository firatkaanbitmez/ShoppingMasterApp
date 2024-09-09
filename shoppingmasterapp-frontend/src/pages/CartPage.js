import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { removeItemFromCart, clearCart } from '../redux/cartSlice';

const CartPage = () => {
  const cartItems = useSelector(state => state.cart.items);
  const totalAmount = cartItems.reduce((total, item) => total + item.quantity * item.price, 0);
  const dispatch = useDispatch();

  const handleRemoveItem = (itemId) => {
    dispatch(removeItemFromCart({ id: itemId }));
  };

  const handleClearCart = () => {
    dispatch(clearCart());
  };

  return (
    <div className="cart-page">
      <h1>Your Cart</h1>
      {cartItems.length === 0 ? (
        <p>No items in the cart.</p>
      ) : (
        <>
          <ul>
            {cartItems.map(item => (
              <li key={item.id}>
                {item.name} - {item.quantity} x {item.price}₺
                <button onClick={() => handleRemoveItem(item.id)}>Remove</button>
              </li>
            ))}
          </ul>
          <h2>Total Amount: {totalAmount}₺</h2>
          <button onClick={handleClearCart}>Clear Cart</button>
        </>
      )}
    </div>
  );
};

export default CartPage;

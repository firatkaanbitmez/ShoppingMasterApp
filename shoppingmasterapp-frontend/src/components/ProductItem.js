import React from 'react';
import { useDispatch } from 'react-redux';
import { addItemToCart } from '../redux/cartSlice';

const ProductItem = ({ product }) => {
  const dispatch = useDispatch();

  const handleAddToCart = () => {
    dispatch(addItemToCart({
      id: product.id,
      name: product.name,
      price: product.price,
      quantity: 1
    }));
  };

  return (
    <div className="product-item">
      <h2>{product.name}</h2>
      <p>{product.description}</p>
      <p>Price: {product.price}₺</p>
      <p>Stock: {product.stock}</p>
      <button onClick={handleAddToCart}>Add to Cart</button>
    </div>
  );
};

export default ProductItem;

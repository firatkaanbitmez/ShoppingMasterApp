import React from 'react';
import { useDispatch } from 'react-redux';
import { addItemToCart } from '../redux/cartSlice';

const ProductItem = ({ product = {} }) => {
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
    <div className="product-card">
      {/* Check if imageUrl exists, otherwise display a placeholder */}
      <img src={product.imageUrl || 'placeholder.jpg'} alt={product.name || 'Product'} />
      <h3>{product.name || 'Product Name'}</h3>
      <p>{product.description || 'No description available'}</p>
      <p className="price">{product.price ? `${product.price}₺` : 'Price Unavailable'}</p>
      <button onClick={handleAddToCart}>Add to Cart</button>
    </div>
  );
};

export default ProductItem;

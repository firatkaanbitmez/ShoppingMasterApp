import React from 'react';

const ProductModal = ({ product, onClose }) => {
  if (!product) return null;

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h2>{product.name}</h2>
        <p>{product.description}</p>
        <p>Price: {product.price}₺</p>
        <p>Stock: {product.stock}</p>
        <button onClick={onClose}>Close</button>
      </div>
    </div>
  );
};

export default ProductModal;

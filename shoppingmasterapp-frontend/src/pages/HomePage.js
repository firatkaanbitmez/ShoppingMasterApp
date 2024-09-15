import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchProducts } from '../redux/productSlice';
import ProductList from '../components/ProductList';
import '../assets/homepage.css';  // Layout stilleri için import

const HomePage = () => {
  const dispatch = useDispatch();
  const products = useSelector((state) => state.products.items);
  const loading = useSelector((state) => state.products.loading);
  const error = useSelector((state) => state.products.error);

  useEffect(() => {
    dispatch(fetchProducts());
  }, [dispatch]);

  return (
    <div className="wrapper"> {/* Wrapper, tüm içeriği kapsayan div */}
      <main className="homepage">
        <div className="banner">
          <h2>Best Deals of the Year!</h2>
        </div>
        <h1>Products</h1>
        {loading ? (
          <p>Loading...</p>
        ) : error ? (
          <p>Error loading products: {error}</p>
        ) : (
          <ProductList products={products} />
        )}
      </main>
    </div>
  );
};

export default HomePage;

import React from 'react';

const Slider = ({ products }) => {
  return (
    <div id="limitedStockSlider" className="carousel slide" data-ride="carousel">
      <div className="carousel-inner">
        {products.map((product, index) => (
          <div key={product.id} className={`carousel-item ${index === 0 ? 'active' : ''}`}>
            <img src={product.imageUrl} alt={product.name} />
            <div className="carousel-caption">
              <h5>{product.name}</h5>
              <p>{product.description}</p>
              <p className="price">
                <del>{product.oldPrice}₺</del> {product.price}₺
              </p>
            </div>
          </div>
        ))}
      </div>
      <a className="carousel-control-prev" href="#limitedStockSlider" role="button" data-slide="prev">
        <span className="carousel-control-prev-icon" aria-hidden="true"></span>
        <span className="sr-only">Previous</span>
      </a>
      <a className="carousel-control-next" href="#limitedStockSlider" role="button" data-slide="next">
        <span className="carousel-control-next-icon" aria-hidden="true"></span>
        <span className="sr-only">Next</span>
      </a>
    </div>
  );
};

export default Slider;

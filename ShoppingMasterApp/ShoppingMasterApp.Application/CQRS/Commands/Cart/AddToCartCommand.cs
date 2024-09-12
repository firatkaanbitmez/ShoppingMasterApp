using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class AddToCartCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int UserId { get; set; }

    public class Handler : IRequestHandler<AddToCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICartRepository cartRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null || product.Stock < request.Quantity)
            {
                throw new KeyNotFoundException("Product not available or insufficient stock.");
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
            if (cart == null)
            {
                // Yeni Cart oluşturuluyor
                cart = new Cart { UserId = request.UserId };
                await _cartRepository.AddAsync(cart);
                await _unitOfWork.SaveChangesAsync();  // Id burada atanmalı
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;  // Miktar güncellenir
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = request.Quantity,
                    UnitPrice = product.Price.Amount
                });
            }

            _cartRepository.Update(cart);  // Sepet güncelleniyor
            await _unitOfWork.SaveChangesAsync();  // Değişiklikler kaydediliyor
            return Unit.Value;
        }

    }
}

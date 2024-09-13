using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class RemoveFromCartCommand : IRequest<Unit>
{
    public int ProductId { get; set; }
    public int CustomerId { get; set; }

    public class Handler : IRequestHandler<RemoveFromCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);  // Listeden silme
                _cartRepository.Update(cart);    // Veritabanında güncelleme
                await _unitOfWork.SaveChangesAsync();  // Değişiklikleri kaydet
            }
            else
            {
                throw new KeyNotFoundException("Product not found in cart.");
            }

            return Unit.Value;
        }
    }
}

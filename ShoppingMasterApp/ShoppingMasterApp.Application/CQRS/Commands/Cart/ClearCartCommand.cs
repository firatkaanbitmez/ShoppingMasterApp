using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public class ClearCartCommand : IRequest<Unit>
{
    public int CustomerId { get; set; }

    public class Handler : IRequestHandler<ClearCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId);
            if (cart == null)
            {
                throw new KeyNotFoundException("Cart not found.");
            }

            cart.CartItems.Clear();  // Tüm ürünleri temizle
            _cartRepository.Update(cart);  // Veritabanında güncelle
            await _unitOfWork.SaveChangesAsync();  // Değişiklikleri kaydet

            return Unit.Value;
        }
    }
}

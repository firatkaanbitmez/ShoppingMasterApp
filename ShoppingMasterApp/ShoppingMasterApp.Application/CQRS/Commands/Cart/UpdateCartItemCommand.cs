using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Cart
{
    public class UpdateCartItemCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int CustomerId { get; set; }

        public class Handler : IRequestHandler<UpdateCartItemCommand, Unit>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICartRepository cartRepository, IUnitOfWork unitOfWork)
            {
                _cartRepository = cartRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
            {
                var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId);
                if (cart == null)
                {
                    throw new KeyNotFoundException("Cart not found.");
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
                if (cartItem == null)
                {
                    throw new KeyNotFoundException("Product not found in cart.");
                }

                cartItem.Quantity = request.Quantity;

                _cartRepository.Update(cart);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

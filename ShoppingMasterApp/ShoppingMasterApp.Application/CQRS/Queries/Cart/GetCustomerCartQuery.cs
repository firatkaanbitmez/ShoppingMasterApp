using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Cart
{
    public class GetCustomerCartQuery : IRequest<CartDto>
    {
        public int CustomerId { get; set; }

        public class Handler : IRequestHandler<GetCustomerCartQuery, CartDto>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IMapper _mapper;

            public Handler(ICartRepository cartRepository, IMapper mapper)
            {
                _cartRepository = cartRepository;
                _mapper = mapper;
            }

            public async Task<CartDto> Handle(GetCustomerCartQuery request, CancellationToken cancellationToken)
            {
                var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId);
                if (cart == null)
                {
                    throw new KeyNotFoundException("Cart not found.");
                }

                return _mapper.Map<CartDto>(cart);
            }
        }
    }
}

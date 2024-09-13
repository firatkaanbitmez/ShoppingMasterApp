using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Cart
{
    public class GetUserCartQuery : IRequest<CartDto>
    {
        public int UserId { get; set; }

        public class Handler : IRequestHandler<GetUserCartQuery, CartDto>
        {
            private readonly ICartRepository _cartRepository;
            private readonly IMapper _mapper;

            public Handler(ICartRepository cartRepository, IMapper mapper)
            {
                _cartRepository = cartRepository;
                _mapper = mapper;
            }

            public async Task<CartDto> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);
                if (cart == null)
                {
                    throw new KeyNotFoundException("Cart not found.");
                }

                return _mapper.Map<CartDto>(cart);
            }
        }
    }
}

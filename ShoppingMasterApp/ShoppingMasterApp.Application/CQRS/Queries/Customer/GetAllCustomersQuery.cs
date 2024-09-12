using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public class Handler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
        {
            private readonly ICustomerRepository _userRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllCustomersAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(users);
            }
        }
    }

}

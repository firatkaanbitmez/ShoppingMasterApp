using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.Customer
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public class Handler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetAllCustomersAsync();
                return _mapper.Map<IEnumerable<CustomerDto>>(customers);
            }
        }
    }

}

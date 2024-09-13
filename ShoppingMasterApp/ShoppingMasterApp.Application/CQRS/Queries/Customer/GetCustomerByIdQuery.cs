using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

namespace ShoppingMasterApp.Application.CQRS.Queries.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetByIdAsync(request.Id);
                if (customer == null)
                {
                    throw new KeyNotFoundException("customer not found");
                }
                return _mapper.Map<CustomerDto>(customer);
            }
        }
    }

}

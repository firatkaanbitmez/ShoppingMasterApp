using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                // Check if email already exists
                var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (existingCustomer != null)
                {
                    throw new ArgumentException("Email already registered.");
                }

                // Hash the password before saving
                var customer = new Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = Roles.Customer,
                    Address = request.Address
                };

                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }

}

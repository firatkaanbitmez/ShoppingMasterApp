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

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _CustomerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
            {
                _CustomerRepository = CustomerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = new Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = Roles.Customer,
                    Address = request.Address
                };

                await _CustomerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

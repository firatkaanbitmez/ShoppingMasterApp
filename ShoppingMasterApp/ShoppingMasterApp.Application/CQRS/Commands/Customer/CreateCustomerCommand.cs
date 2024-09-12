using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
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
            private readonly ICustomerRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var user = new Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = Roles.Customer, // Sabit Rol Customer
                    Address = request.Address
                };

                await _userRepository.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

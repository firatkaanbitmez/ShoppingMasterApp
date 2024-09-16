using MediatR;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }

        // Handler embedded inside the command class
        public class Handler : IRequestHandler<UpdateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository.GetByIdAsync(request.Id);
                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }

                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Email = new Email(request.Email);
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                customer.Roles = Roles.Customer;
                customer.Address = request.Address;

                _customerRepository.Update(customer);  // Using the generic repository's Update method
                await _unitOfWork.SaveChangesAsync();   // Save changes asynchronously

                return Unit.Value;
            }
        }
    }
}

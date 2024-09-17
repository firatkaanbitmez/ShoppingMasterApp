using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class ChangePasswordCommand : IRequest<Unit>  
    {
        public int CustomerId { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Unit>  
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null) throw new Exception("Customer not found");

            // Hash and update the password
            customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;  // Return Unit.Value to indicate success
        }
    }
}

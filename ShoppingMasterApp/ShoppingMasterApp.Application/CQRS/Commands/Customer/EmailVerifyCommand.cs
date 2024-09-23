using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class EmailVerifyCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }

        public class Handler : IRequestHandler<EmailVerifyCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IEmailVerificationService _emailVerificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                ICustomerRepository customerRepository,
                IEmailVerificationService emailVerificationService,
                IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _emailVerificationService = emailVerificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(EmailVerifyCommand request, CancellationToken cancellationToken)
            {
                // Find the customer by email
                var customer = await _customerRepository.GetCustomerByEmailAsync(request.Email);

                if (customer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }

                // Check if the email verification code has expired
                if (customer.EmailVerificationExpiryDate < DateTime.UtcNow)
                {
                    throw new ArgumentException("Email verification code expired.");
                }

                // Check if the provided verification code is correct
                if (customer.EmailVerificationCode != request.VerificationCode)
                {
                    throw new ArgumentException("Invalid email verification code.");
                }

                // Mark email as verified
                customer.IsEmailVerified = true;

                _customerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

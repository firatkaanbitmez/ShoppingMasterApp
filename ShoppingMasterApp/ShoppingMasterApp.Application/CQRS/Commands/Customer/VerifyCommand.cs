using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class VerifyCommand : IRequest<Unit>
    {
        public string Identifier { get; set; } // Can be an email or phone number
        public string VerificationCode { get; set; }

        public class Handler : IRequestHandler<VerifyCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(VerifyCommand request, CancellationToken cancellationToken)
            {
                // Determine if the identifier is an email or phone number
                bool isEmail = request.Identifier.Contains("@");
                var customer = isEmail
                    ? await _customerRepository.GetCustomerByEmailAsync(request.Identifier)
                    : await _customerRepository.GetCustomerByPhoneNumberAsync(request.Identifier);

                if (customer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }

                // Handle email verification
                if (isEmail)
                {
                    if (customer.EmailVerificationCode != request.VerificationCode)
                    {
                        throw new ArgumentException("Invalid email verification code.");
                    }

                    if (customer.EmailVerificationExpiryDate < DateTime.UtcNow)
                    {
                        throw new ArgumentException("Email verification code expired.");
                    }

                    customer.IsEmailVerified = true;
                }
                // Handle SMS verification
                else
                {
                    if (customer.SmsVerificationCode != request.VerificationCode)
                    {
                        throw new ArgumentException("Invalid SMS verification code.");
                    }

                    if (customer.SmsVerificationExpiryDate < DateTime.UtcNow)
                    {
                        throw new ArgumentException("SMS verification code expired.");
                    }

                    customer.IsSmsVerified = true;
                }

                _customerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

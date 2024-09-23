using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IEmailVerificationService _emailVerificationService;
            private readonly ISmsVerificationService _smsVerificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IEmailVerificationService emailVerificationService, ISmsVerificationService smsVerificationService, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _emailVerificationService = emailVerificationService;
                _smsVerificationService = smsVerificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                // Check if email or phone number already exists
                var existingCustomerByEmail = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (existingCustomerByEmail != null)
                {
                    throw new ArgumentException("This email address is already registered. You cannot register with the same email.");
                }

                var existingCustomerByPhone = await _customerRepository.GetCustomerByPhoneNumberAsync(request.PhoneNumber.CountryCode, request.PhoneNumber.Number);
                if (existingCustomerByPhone != null)
                {
                    throw new ArgumentException("This phone number is already registered. You cannot register with the same phone number.");
                }

                var emailVerificationCode = Guid.NewGuid().ToString().Substring(0, 6);
                var smsVerificationCode = Guid.NewGuid().ToString().Substring(0, 6);
                var expiryDuration = 5; // Expiration in minutes
                var expiryDate = DateTime.UtcNow.AddMinutes(expiryDuration);

                var customer = new ShoppingMasterApp.Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    EmailVerificationCode = emailVerificationCode,
                    EmailVerificationExpiryDate = expiryDate,
                    SmsVerificationCode = smsVerificationCode,
                    SmsVerificationExpiryDate = expiryDate,
                    IsEmailVerified = false,
                    IsSmsVerified = false
                };

                // Add new customer to the repository
                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                // Send email verification
                if (!string.IsNullOrEmpty(request.Email))
                {
                    var emailMessage = $"Dear {customer.FirstName}, your email verification code is: {emailVerificationCode}  \n This code is valid for {expiryDuration} minute(s).";
                    await _emailVerificationService.SendVerificationEmailAsync(request.Email, "Email Verification", emailMessage, $"<p>{emailMessage}</p>");
                }

                // Send SMS verification
                if (!string.IsNullOrEmpty(request.PhoneNumber.Number))
                {
                    var fullPhoneNumber = request.PhoneNumber.GetFullNumber();
                    await _smsVerificationService.SendVerificationSmsAsync(fullPhoneNumber, smsVerificationCode);
                }

                return Unit.Value;
            }
        }
    }
}

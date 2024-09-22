using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.Entities;
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
           // private readonly ISmsVerificationService _smsVerificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IEmailVerificationService emailVerificationService,  IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _emailVerificationService = emailVerificationService;
              //  _smsVerificationService = smsVerificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var emailVerificationCode = Guid.NewGuid().ToString().Substring(0, 6);
                //var smsVerificationCode = Guid.NewGuid().ToString().Substring(0, 6);
                var expiryDate = DateTime.UtcNow.AddMinutes(30);

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
                    //SmsVerificationCode = smsVerificationCode,
                    //SmsVerificationExpiryDate = expiryDate,
                    IsEmailVerified = false,
                    IsSmsVerified = false
                };

                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                if (!string.IsNullOrEmpty(request.Email))
                {
                    var emailMessage = $"Sayın {customer.FirstName}, email doğrulama kodunuz: {emailVerificationCode}. Bu kod 30 dakika geçerlidir.";
                    await _emailVerificationService.SendVerificationEmailAsync(request.Email, "Email Doğrulama", emailMessage, $"<p>{emailMessage}</p>");
                }

                //if (!string.IsNullOrEmpty(request.PhoneNumber.Number))
                //{
                //    var smsMessage = $"Sayın {customer.FirstName}, SMS doğrulama kodunuz: {smsVerificationCode}. Bu kod 30 dakika geçerlidir.";
                //    await _smsVerificationService.SendVerificationSmsAsync(request.PhoneNumber.Number, smsMessage);
                //}

                return Unit.Value;
            }
        }
    }
}

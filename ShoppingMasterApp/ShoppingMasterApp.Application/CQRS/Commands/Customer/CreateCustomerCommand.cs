using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System;
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
        public Address Address { get; set; }  // Address alanı burada eklenmiş durumda

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IEmailService _emailService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(ICustomerRepository customerRepository, IEmailService emailService, IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _emailService = emailService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (existingCustomer != null)
                {
                    // Burada 400 BadRequest dönecek şekilde bir hata mesajı oluşturuyoruz.
                    throw new ArgumentException("Bu e-posta adresi zaten kayıtlı.", nameof(request.Email));
                }

                var verificationCode = new Random().Next(100000, 999999).ToString(); // 6 haneli doğrulama kodu
                var customer = new ShoppingMasterApp.Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = Roles.Customer,
                    Address = request.Address,
                    VerificationCode = verificationCode,
                    IsVerified = false
                };

                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                var emailMessage = $"Merhaba {customer.FirstName},\n\nKayıt işleminizi tamamlamak için doğrulama kodunuz: {verificationCode}";
                await _emailService.SendEmailAsync(customer.Email.Value, "E-posta Doğrulama", emailMessage);

                return Unit.Value;
            }

        }
    }
}

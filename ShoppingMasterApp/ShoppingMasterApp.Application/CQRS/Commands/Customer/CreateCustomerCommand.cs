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
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IVerificationService _verificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                ICustomerRepository customerRepository,
                IVerificationService verificationService,
                IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _verificationService = verificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var existingCustomerByEmail = await _customerRepository.GetCustomerByEmailAsync(request.Email);
                if (existingCustomerByEmail != null)
                    throw new ArgumentException("This email is already registered.");

                var existingCustomerByPhone = await _customerRepository.GetCustomerByPhoneNumberAsync(request.PhoneNumber.CountryCode, request.PhoneNumber.Number);
                if (existingCustomerByPhone != null)
                    throw new ArgumentException("This phone number is already registered.");

                var customer = new ShoppingMasterApp.Domain.Entities.Customer
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber
                  
                };

                await _verificationService.SendVerificationCodeAsync(customer, VerificationType.Email);
                await _verificationService.SendVerificationCodeAsync(customer, VerificationType.Sms);

                await _customerRepository.AddAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

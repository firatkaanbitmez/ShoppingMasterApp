using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Customer
{
    public class SmsVerifyCommand : IRequest<Unit>
    {
        public string CountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }

        public class Handler : IRequestHandler<SmsVerifyCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ISmsVerificationService _smsVerificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                ICustomerRepository customerRepository,
                ISmsVerificationService smsVerificationService,
                IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _smsVerificationService = smsVerificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(SmsVerifyCommand request, CancellationToken cancellationToken)
            {
                // Find the customer by phone number
                var customer = await _customerRepository.GetCustomerByPhoneNumberAsync(request.CountryCode, request.PhoneNumber);

                if (customer == null)
                {
                    throw new ArgumentException("Customer not found.");
                }

                // Check if the SMS verification code has expired
                if (customer.SmsVerificationExpiryDate < DateTime.UtcNow)
                {
                    throw new ArgumentException("SMS verification code expired.");
                }

                // Check if the provided verification code is valid
                var isVerified = await _smsVerificationService.VerifyCodeAsync(customer.PhoneNumber.GetFullNumber(), request.VerificationCode);
                if (!isVerified)
                {
                    throw new ArgumentException("Invalid SMS verification code.");
                }

                // Mark SMS as verified
                customer.IsSmsVerified = true;

                _customerRepository.Update(customer);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}

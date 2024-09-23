using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Application.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands
{
    public class VerifyCodeCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public VerificationType VerificationType { get; set; }
        public string Code { get; set; }

        public class Handler : IRequestHandler<VerifyCodeCommand, Unit>
        {
            private readonly IVerificationService _verificationService;
            private readonly ICustomerRepository _customerRepository;
            private readonly IAdminRepository _adminRepository;
            private readonly ISmsVerificationService _smsVerificationService; // SMS servisi eklendi
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IVerificationService verificationService,
                ICustomerRepository customerRepository,
                IAdminRepository adminRepository,
                ISmsVerificationService smsVerificationService, // Dependency Injection
                IUnitOfWork unitOfWork)
            {
                _verificationService = verificationService;
                _customerRepository = customerRepository;
                _adminRepository = adminRepository;
                _smsVerificationService = smsVerificationService; // Atama yapıldı
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
            {
                BaseUser user = await GetUserByIdAsync(request.UserId);
                if (user == null)
                    throw new ArgumentException("Kullanıcı bulunamadı.");

                // Kod doğrulandıktan sonra eğer email veya SMS doğrulandıysa güncellemeyi yapalım
                if (request.VerificationType == VerificationType.Email)
                {
                    if (user.EmailVerificationExpiryDate < DateTime.UtcNow)
                        throw new ArgumentException("Email doğrulama kodu süresi doldu.");

                    if (user.EmailVerificationCode != request.Code)
                        throw new ArgumentException("Geçersiz email doğrulama kodu.");

                    user.IsEmailVerified = true; // Email doğrulandı
                }
                else if (request.VerificationType == VerificationType.Sms)
                {
                    if (user.SmsVerificationExpiryDate < DateTime.UtcNow)
                        throw new ArgumentException("SMS doğrulama kodu süresi doldu.");

                    var isValid = await _smsVerificationService.VerifyCodeAsync(user.PhoneNumber.GetFullNumber(), request.Code);
                    if (!isValid)
                        throw new ArgumentException("Geçersiz SMS doğrulama kodu.");

                    user.IsSmsVerified = true; // SMS doğrulandı
                }

                // Kullanıcıyı güncelle ve veritabanına kaydet
                if (user is ShoppingMasterApp.Domain.Entities.Customer customer)
                {
                    _customerRepository.Update(customer);
                }
                else if (user is ShoppingMasterApp.Domain.Entities.Admin admin)
                {
                    _adminRepository.Update(admin);
                }


                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }


            private async Task<BaseUser> GetUserByIdAsync(int userId)
            {
                var customer = await _customerRepository.GetByIdAsync(userId);
                if (customer != null)
                    return customer;

                var admin = await _adminRepository.GetByIdAsync(userId);
                return admin;
            }
        }
    }
}

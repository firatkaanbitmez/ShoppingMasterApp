using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;
using ShoppingMasterApp.Application.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using ShoppingMasterApp.Domain.Enums;

namespace ShoppingMasterApp.Application.CQRS.Commands.Admin
{
    public class CreateAdminCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PhoneNumber PhoneNumber { get; set; }

        public class Handler : IRequestHandler<CreateAdminCommand, Unit>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IVerificationService _verificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                IAdminRepository adminRepository,
                IVerificationService verificationService,
                IUnitOfWork unitOfWork)
            {
                _adminRepository = adminRepository;
                _verificationService = verificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
            {
                // Check if email already exists
                var existingAdmin = await _adminRepository.GetAdminByEmailAsync(request.Email);
                if (existingAdmin != null)
                {
                    throw new ArgumentException("This email address is already registered.");
                }

                // Create a new admin
                var admin = new ShoppingMasterApp.Domain.Entities.Admin
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    PhoneNumber = request.PhoneNumber,
                    IsEmailVerified = false,
                    IsSmsVerified = false
                };

                // Add new admin to repository
                await _adminRepository.AddAsync(admin);
                await _unitOfWork.SaveChangesAsync();

                // Send verification codes for email and SMS
                await _verificationService.SendVerificationCodeAsync(admin, VerificationType.Email);
                await _verificationService.SendVerificationCodeAsync(admin, VerificationType.Sms);

                return Unit.Value;
            }
        }
    }
}

using MediatR;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands
{
    public class SendVerificationCodeCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public VerificationType VerificationType { get; set; }

        public class Handler : IRequestHandler<SendVerificationCodeCommand, Unit>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IAdminRepository _adminRepository;
            private readonly IVerificationService _verificationService;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(
                ICustomerRepository customerRepository,
                IAdminRepository adminRepository,
                IVerificationService verificationService,
                IUnitOfWork unitOfWork)
            {
                _customerRepository = customerRepository;
                _adminRepository = adminRepository;
                _verificationService = verificationService;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
            {
                BaseUser user = await GetUserById(request.UserId);
                if (user == null)
                    throw new ArgumentException("User not found.");

                await _verificationService.SendVerificationCodeAsync(user, request.VerificationType);
                await _unitOfWork.SaveChangesAsync();

                return Unit.Value;
            }

            private async Task<BaseUser> GetUserById(int userId)
            {
                var customer = await _customerRepository.GetByIdAsync(userId);
                if (customer != null) return customer;

                var admin = await _adminRepository.GetByIdAsync(userId);
                return admin;
            }
        }
    }

}

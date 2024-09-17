using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Admin
{
    public class CreateAdminCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<CreateAdminCommand, Unit>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
            {
                _adminRepository = adminRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
            {
                var existingAdmin = await _adminRepository.GetAdminByEmailAsync(request.Email);
                if (existingAdmin != null)
                {
                    throw new ArgumentException("Email already registered.");
                }

                var admin = new ShoppingMasterApp.Domain.Entities.Admin
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = new Email(request.Email),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Roles = Roles.Admin
                };

                await _adminRepository.AddAsync(admin);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}

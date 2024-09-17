using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Admin
{
    public class UpdateAdminCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<UpdateAdminCommand, Unit>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
            {
                _adminRepository = adminRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
            {
                var admin = await _adminRepository.GetByIdAsync(request.Id);
                if (admin == null)
                {
                    throw new KeyNotFoundException("Admin not found");
                }

                admin.FirstName = request.FirstName;
                admin.LastName = request.LastName;
                admin.Email = new Email(request.Email);
                admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                _adminRepository.Update(admin);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}

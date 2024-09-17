using MediatR;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Admin
{
    public class DeleteAdminCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteAdminCommand, Unit>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
            {
                _adminRepository = adminRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
            {
                var admin = await _adminRepository.GetByIdAsync(request.Id);
                if (admin == null)
                {
                    throw new KeyNotFoundException("Admin not found");
                }

                _adminRepository.Delete(admin);
                await _unitOfWork.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}

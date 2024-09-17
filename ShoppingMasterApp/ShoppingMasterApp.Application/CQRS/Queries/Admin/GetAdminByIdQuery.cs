using MediatR;
using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Admin
{
    public class GetAdminByIdQuery : IRequest<AdminDto>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetAdminByIdQuery, AdminDto>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IMapper _mapper;

            public Handler(IAdminRepository adminRepository, IMapper mapper)
            {
                _adminRepository = adminRepository;
                _mapper = mapper;
            }

            public async Task<AdminDto> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
            {
                var admin = await _adminRepository.GetByIdAsync(request.Id);
                if (admin == null)
                {
                    throw new KeyNotFoundException("Admin not found");
                }

                return _mapper.Map<AdminDto>(admin);
            }
        }
    }
}

using MediatR;
using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Queries.Admin
{
    public class GetAllAdminsQuery : IRequest<IEnumerable<AdminDto>>
    {
        public class Handler : IRequestHandler<GetAllAdminsQuery, IEnumerable<AdminDto>>
        {
            private readonly IAdminRepository _adminRepository;
            private readonly IMapper _mapper;

            public Handler(IAdminRepository adminRepository, IMapper mapper)
            {
                _adminRepository = adminRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<AdminDto>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
            {
                var admins = await _adminRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<AdminDto>>(admins);
            }
        }
    }
}

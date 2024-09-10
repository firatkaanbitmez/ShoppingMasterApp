using AutoMapper;
using MediatR;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;

namespace ShoppingMasterApp.Application.CQRS.Queries.User
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public class Handler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public Handler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserDto>>(users);
            }
        }
    }
}

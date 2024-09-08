using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.CQRS.Queries.User;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Create new user
        public async Task CreateUserAsync(CreateUserCommand command)
        {
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = new Email(command.Email),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Roles = (Roles)Enum.Parse(typeof(Roles), command.Roles),
                Address = command.Address
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        // Update existing user
        public async Task UpdateUserAsync(UpdateUserCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = new Email(command.Email);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);
            user.Roles = (Roles)Enum.Parse(typeof(Roles), command.Roles);
            user.Address = command.Address;

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        // Delete user
        public async Task DeleteUserAsync(DeleteUserCommand command)
        {
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
        }

        // Get user by ID
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        // Get all users
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        // Get users by role
        public async Task<IEnumerable<UserDto>> GetUsersByRoleAsync(string role)
        {
            var users = await _userRepository.GetUsersByRoleAsync((Roles)Enum.Parse(typeof(Roles), role));
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}

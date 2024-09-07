using ShoppingMasterApp.Application.CQRS.Commands.User;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserProfileAsync(User user)
    {
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser != null)
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            _userRepository.Update(existingUser);
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
    {
        return await _userRepository.GetUsersByRoleAsync(role);
    }

    public async Task CreateUserAsync(CreateUserCommand command)
    {
        var newUser = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PasswordHash = HashPassword(command.Password) // Şifre hash'leniyor
        };
        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public async Task UpdateUserAsync(UpdateUserCommand command)
    {
        var existingUser = await _userRepository.GetByIdAsync(command.Id);
        if (existingUser != null)
        {
            existingUser.FirstName = command.FirstName;
            existingUser.LastName = command.LastName;
            existingUser.Email = command.Email;
            _userRepository.Update(existingUser);
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}

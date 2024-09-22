using Microsoft.Extensions.Configuration;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Infrastructure.Seeders
{
    public class AdminSeeder
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AdminSeeder(IAdminRepository adminRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task SeedAdminAsync()
        {
            var existingAdmins = await _adminRepository.GetAllAsync();
            if (!existingAdmins.Any())
            {
                var adminEmail = _configuration["AdminSeed:Email"];
                var adminPassword = _configuration["AdminSeed:Password"];

                var admin = new Admin
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Email = new Email(adminEmail),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPassword),
                    Roles = Roles.Admin,
                    EmailVerificationCode = "123456",
                    SmsVerificationCode ="123456",
                    PhoneNumber = PhoneNumber.Create("+1", "1234567890") 


                };

                await _adminRepository.AddAsync(admin);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}

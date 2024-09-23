using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _context.Customers.FirstOrDefaultAsync(u => u.Email.Value == email);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByRoleAsync(Roles role)
        {
            return await _context.Customers.Where(u => u.Roles == role).ToListAsync();
        }
        public async Task<Customer> GetCustomerByPhoneNumberAsync(string countryCode, string number)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(u => u.PhoneNumber.CountryCode == countryCode && u.PhoneNumber.Number == number);
        }








    }
}

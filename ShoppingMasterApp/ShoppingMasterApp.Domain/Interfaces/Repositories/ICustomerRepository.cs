using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByRoleAsync(Roles role);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}

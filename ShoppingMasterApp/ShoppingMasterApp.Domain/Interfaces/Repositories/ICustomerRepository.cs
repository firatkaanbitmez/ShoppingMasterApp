using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.Interfaces.Repositories;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IEnumerable<Customer>> GetCustomersByRoleAsync(Roles role);
    Task<Customer> GetCustomerByEmailAsync(string email);
    Task<Customer> GetCustomerByPhoneNumberAsync(string countryCode, string number); 
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
}

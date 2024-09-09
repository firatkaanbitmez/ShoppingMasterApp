using ShoppingMasterApp.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class CreateUserCommand
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Roles { get; set; }
        public Address Address { get; set; }
    }
}


namespace ShoppingMasterApp.Application.DTOs
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public AddressDto Address { get; set; }
    }

}

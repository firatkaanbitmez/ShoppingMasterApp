using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class UpdateUserCommand
    {
        public int Id { get; set; }  // Güncellenen kullanıcının ID'si
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }  // Kullanıcı rolü
        public Address Address { get; set; }  // Adres bilgisi
    }
}

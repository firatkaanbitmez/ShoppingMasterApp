using ShoppingMasterApp.Domain.Enums;
namespace ShoppingMasterApp.Domain.Entities
{
    public class Admin : BaseUser
    {
        public Admin() : base(Roles.Admin) { }
    }
}
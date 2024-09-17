using ShoppingMasterApp.Domain.Common;
using ShoppingMasterApp.Domain.Enums;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;


namespace ShoppingMasterApp.Domain.Entities
{
    public class Customer : BaseUser
    {
        public Address Address { get; set; }  // Customer'a özgü adres bilgisi eklenebilir.

        public Customer() : base(Roles.Customer) { }

    }
}

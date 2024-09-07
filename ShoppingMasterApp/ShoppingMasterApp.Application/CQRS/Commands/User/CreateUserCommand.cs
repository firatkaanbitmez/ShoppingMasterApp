using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.User
{
    public class CreateUserCommand
    {
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string Email { get; internal set; }
        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (value.Length < 6)
                    throw new ArgumentException("Password must be at least 6 characters long.");
                _password = value;
            }
        }
    }

}

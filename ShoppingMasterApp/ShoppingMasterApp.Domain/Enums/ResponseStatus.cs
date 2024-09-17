using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Domain.Enums
{
    public enum ResponseStatus
    {
        Success,
        Error,
        ValidationError,
        NotFound,
        Unauthorized,
        ServerError
    }

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.CQRS.Commands.Discount
{
    public class ApplyDiscountCommand 
    {
        public string DiscountCode { get; set; }
        public int CustomerId { get; set; }
    }
}

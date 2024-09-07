using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Category;
using ShoppingMasterApp.Application.CQRS.Commands.Order;
using ShoppingMasterApp.Application.CQRS.Commands.Product;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
        }
    }


}

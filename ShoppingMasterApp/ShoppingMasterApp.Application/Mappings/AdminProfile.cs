using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;

namespace ShoppingMasterApp.Application.Mappings
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToString()));
        }
    }
}

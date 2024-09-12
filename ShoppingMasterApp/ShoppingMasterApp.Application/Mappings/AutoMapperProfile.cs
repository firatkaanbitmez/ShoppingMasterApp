using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Cart Mappings
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            // CartItem Mappings
            CreateMap<CartItem, CartItemDto>();

            // Category Mappings
            CreateMap<Category, CategoryDto>();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.ProductDetails.Manufacturer))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.ProductDetails.Sku))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));

            // Review Mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName));

            // Shipping Mappings
            CreateMap<Shipping, ShippingDto>()
            .ForMember(dest => dest.ShippingAddress,
               opt => opt.MapFrom(src => $"{src.ShippingAddress.AddressLine1}, {src.ShippingAddress.City}, {src.ShippingAddress.State}"));

            // Payment Mappings
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate));


            // Order Mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalAmount.Amount));

            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemDto>();

            // Add mapping for Address -> AddressDto
            CreateMap<Address, AddressDto>();

            // User Mappings
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        }
    }
}

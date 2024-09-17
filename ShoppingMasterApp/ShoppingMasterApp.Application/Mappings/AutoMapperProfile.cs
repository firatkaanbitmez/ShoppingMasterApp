using AutoMapper;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.ValueObjects;

namespace ShoppingMasterApp.Application.Mappings
{
    // AutoMapper Profili: Tüm mapping işlemleri bu profile eklenir
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Admin Mappings
            CreateMap<Admin, AdminDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value)) 
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToString()));


            // Customer Mappings
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.ToString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));


            // Category Mappings
            CreateMap<Category, CategoryDto>();


            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Manufacturer, opt => opt.MapFrom(src => src.ProductDetails.Manufacturer))
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.ProductDetails.Sku))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Amount));


            // Cart Mappings
            CreateMap<Cart, CartDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice)); 


            // CartItem Mappings
            CreateMap<CartItem, CartItemDto>();
            

            // Review Mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)) 
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName)); 


            // Shipping Mappings
            CreateMap<Shipping, ShippingDto>()
                .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src =>
                    $"{src.ShippingAddress.AddressLine1}, {src.ShippingAddress.City}, {src.ShippingAddress.State}")); 


            // Payment Mappings
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate)); 


            // Order Mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalAmount.Amount)); 


            // OrderItem Mappings
            CreateMap<OrderItem, OrderItemDto>();


            // Address Mappings
            CreateMap<Address, AddressDto>();



        }
    }
}

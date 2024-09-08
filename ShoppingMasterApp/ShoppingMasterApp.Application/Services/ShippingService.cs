using AutoMapper;
using ShoppingMasterApp.Application.CQRS.Commands.Shipping;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces.Services;
using ShoppingMasterApp.Domain.Entities;
using ShoppingMasterApp.Domain.Interfaces.Repositories;
using ShoppingMasterApp.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ShippingService : IShippingService
{
    private readonly IShippingRepository _shippingRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ShippingService(IShippingRepository shippingRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _shippingRepository = shippingRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

   
}

﻿using MediatR;
using ProductEntity = ShoppingMasterApp.Domain.Entities.Product;

namespace ShoppingMasterApp.Application.CQRS.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductEntity>
    {
        public int Id { get; set; }
    }
}

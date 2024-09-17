using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Review;
using ShoppingMasterApp.Application.DTOs;
using ShoppingMasterApp.Application.Interfaces;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : BaseController
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator, ITokenService tokenService)
     : base(tokenService)
        {
            _mediator = mediator;
        }

    }
}

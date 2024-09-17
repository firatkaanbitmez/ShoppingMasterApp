﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.CQRS.Commands.Admin;
using ShoppingMasterApp.Application.CQRS.Queries.Admin;
using System.Threading.Tasks;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Yalnızca Admin rolüne sahip kullanıcılar bu endpoint'e erişebilir
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateAdminCommand command)
        {
            await _mediator.Send(command);
            return ApiResponse("Admin başarıyla kaydedildi.");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAdminCommand command)
        {
            var token = await _mediator.Send(command);
            return ApiResponse(token, "Giriş başarılı.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, [FromBody] UpdateAdminCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return ApiResponse("Admin başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            await _mediator.Send(new DeleteAdminCommand { Id = id });
            return ApiResponse("Admin başarıyla silindi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(int id)
        {
            var admin = await _mediator.Send(new GetAdminByIdQuery { Id = id });
            return ApiResponse(admin);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            var admins = await _mediator.Send(new GetAllAdminsQuery());
            return ApiResponse(admins);
        }
    }
}

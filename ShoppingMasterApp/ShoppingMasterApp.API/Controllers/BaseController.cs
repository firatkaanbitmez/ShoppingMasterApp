﻿using Microsoft.AspNetCore.Mvc;
using ShoppingMasterApp.Application.Constants;
using ShoppingMasterApp.Application.Interfaces;
using ShoppingMasterApp.Domain.Enums;
using System.Collections.Generic;
using System.Security.Claims;
using ShoppingMasterApp.API.Responses;

namespace ShoppingMasterApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        protected BaseController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected IActionResult ApiResponse<T>(T result, string message = null, int statusCode = 200)
        {
            return StatusCode(statusCode, new ApiResponse<T>
            {
                StatusCode = statusCode,
                IsSuccess = true,
                Data = result,
                ErrorMessage = null,
                ResponseStatus = ResponseStatus.Success.ToString(),
                Message = message
            });
        }

        protected IActionResult ApiError(string message, int statusCode = 400, ResponseStatus responseStatus = ResponseStatus.Error)
        {
            return StatusCode(statusCode, new ApiResponse<object>
            {
                StatusCode = statusCode,
                IsSuccess = false,
                ErrorMessage = message,
                ResponseStatus = responseStatus.ToString(),
                Data = null
            });
        }

        protected IActionResult ApiValidationError(IEnumerable<string> errors)
        {
            return BadRequest(new ApiResponse<object>
            {
                StatusCode = 400,
                IsSuccess = false,
                ErrorMessage = ErrorMessages.ValidationError,
                ResponseStatus = ResponseStatus.ValidationError.ToString(),
                Data = new { Errors = errors }
            });
        }

        protected int GetUserIdFromToken()
        {
            return _tokenService.GetUserIdFromToken(User);
        }

    }
}

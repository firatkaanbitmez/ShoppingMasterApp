﻿using System.Threading.Tasks;

namespace ShoppingMasterApp.Application.Interfaces
{
    public interface ISmsVerificationService
    {
        Task SendVerificationSmsAsync(string phoneNumber, string message);
    }
}

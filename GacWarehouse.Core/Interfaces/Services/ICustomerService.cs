using GacWarehouse.Core.Entities;
using GacWarehouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<GeneralResponse<LoginResponse>> Login(LoginRequest loginRequest);
        Task<GeneralResponse<ProfileResponse>> GetProfile(string username);
    }
}

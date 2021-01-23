﻿using GacWarehouse.Core.Entities;
using GacWarehouse.Core.Interfaces.Repositories;
using GacWarehouse.Core.Interfaces.Services;
using GacWarehouse.Core.Models;
using GacWarehouse.Data.Helpers;
using GacWarehouse.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GeneralResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var respnose = new GeneralResponse<LoginResponse>();
            
            if (string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var customer = _customerRepository.GetCustomerByUsername(loginRequest.Username);

            if (customer == null)
            {
                respnose.Message = "Invalid Username or Pasword";
                return respnose;
            }

            var isVerified = HashingHelper.VerifyPasswordHashAndSalt(loginRequest.Password, customer.PasswordHash, customer.PasswordSalt);

            if (!isVerified)
            {
                respnose.Message = "Invalid Username or Pasword";
                return respnose;
            }

            respnose = new GeneralResponse<LoginResponse>()
            {
                Success = true,
                Data = new LoginResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName }
            };

            return respnose;
        }

        public async Task<GeneralResponse<ProfileResponse>> GetProfile(string username)
        {
            var respnose = new GeneralResponse<ProfileResponse>();

            if (string.IsNullOrWhiteSpace(username))
            {
                respnose.Message = "Validaton Error";
                return respnose;
            }

            var customer = _customerRepository.GetCustomerByUsername(username);

            if (customer == null)
            {
                respnose.Message = "Customer Not Found";
                return respnose;
            }

            respnose = new GeneralResponse<ProfileResponse>()
            {
                Success = true,
                Data = new ProfileResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName }
            };

            return respnose;
        }        
    }
}

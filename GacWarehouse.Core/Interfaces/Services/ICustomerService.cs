using GacWarehouse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Customer> Authenticate(string username, string password);
        Task<IEnumerable<Customer>> GetAll();
    }
}

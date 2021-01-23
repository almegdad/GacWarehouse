using GacWarehouse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GacWarehouse.Core.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByUsername(string username);
    }
}

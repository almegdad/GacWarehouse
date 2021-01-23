using GacWarehouse.Core.Entities;
using GacWarehouse.Core.Interfaces.Repositories;
using GacWarehouse.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GacWarehouse.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GacWarehouseDbContext _db;

        public CustomerRepository(GacWarehouseDbContext db)
        {
            _db = db;
        }
       
        public Customer GetCustomerByUsername(string username)
        {
            return _db.Customers.SingleOrDefault(a => a.Username.ToLower() == username.ToLower());
        }
    }
}

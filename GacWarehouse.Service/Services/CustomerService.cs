using GacWarehouse.Core.Entities;
using GacWarehouse.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GacWarehouse.Service.Services
{
    public class CustomerService : ICustomerService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Customer> _users = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "Test", LastName = "User", Username = "test", PasswordHash = "test" }
        };

        public async Task<Customer> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.PasswordHash == password));

            if (user == null)
                return null;

            return user;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Task.Run(() => _users);
        }
    }
}

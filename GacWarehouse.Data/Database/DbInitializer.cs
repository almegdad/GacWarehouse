using GacWarehouse.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GacWarehouse.Data.Database
{
    public static class DbInitializer
    {
        public static void Initialize(GacWarehouseDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            var customers = new Customer[]
            {
                new Customer{Username="Customer A",FirstName="Customer", LastName = "A",PasswordHash="",PasswordSalt="", CreateDate= DateTime.Now},
                new Customer{Username="Customer B",FirstName="Customer", LastName = "B",PasswordHash="",PasswordSalt="", CreateDate= DateTime.Now}
            };

            foreach (var c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
        }
    }
}

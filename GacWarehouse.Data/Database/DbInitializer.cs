using GacWarehouse.Core.Entities;
using GacWarehouse.Data.Helpers;
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
                return;   //already seed
            }

            string password = "password123";
            
            string passowrdHash1;
            string passwordSalt1;
            HashingHelper.CreatePasswordHashAndSalt(password, out passowrdHash1, out passwordSalt1);
            
            string passowrdHash2;
            string passwordSalt2;
            HashingHelper.CreatePasswordHashAndSalt(password, out passowrdHash2, out passwordSalt2);

            var customers = new Customer[]
            {
                new Customer{Username="CustomerA",FirstName="Customer", LastName = "A",PasswordHash=passowrdHash1,PasswordSalt=passwordSalt1, CreateDate= DateTime.Now},
                new Customer{Username="CustomerB",FirstName="Customer", LastName = "B",PasswordHash=passowrdHash2,PasswordSalt=passwordSalt2, CreateDate= DateTime.Now}
            };

            foreach (var c in customers)
            {
                context.Customers.Add(c);
            }

            context.SaveChanges();
        }
    }
}

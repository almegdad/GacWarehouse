using GacWarehouse.Core.Entities;
using GacWarehouse.Core.Interfaces.Repositories;
using GacWarehouse.Data.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GacWarehouse.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GacWarehouseDbContext _db;

        public OrderRepository(GacWarehouseDbContext db)
        {
            _db = db;
        }

        public SalesOrder CreateNewOrder(SalesOrder orderModel)
        {
            SalesOrder order = null;
            var isSavedSuccessfully = false;

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    _db.SalesOrders.Add(orderModel);
                    _db.SaveChanges();

                    foreach (var d in orderModel.SalesOrderDetails)
                    {
                        var product = GetProductById(d.ProductId);
                        product.Quantity -= d.Quantity;

                        _db.Products.Update(product);
                    }
                    _db.SaveChanges();

                    transaction.Commit();
                    isSavedSuccessfully = true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

            if (isSavedSuccessfully)
            {
                order = _db.SalesOrders.Include(i => i.SalesOrderDetails).ThenInclude(it => it.Product).SingleOrDefault(a => a.Id == orderModel.Id);
            }

            return order;
        }

        public Product GetProductById(int id)
        {
            return _db.Products.SingleOrDefault(a => a.Id == id);
        }

        public SalesOrder GetOrderDetails(int orderId, int customerId)
        {
            return _db.SalesOrders.Include(i => i.SalesOrderDetails)
                                  .ThenInclude(it => it.Product)
                                  .SingleOrDefault(a => a.Id == orderId && a.CustomerId == customerId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GacWarehouse.Data.Database;
using Microsoft.EntityFrameworkCore;
using GacWarehouse.Data.Repositories;
using GacWarehouse.Service.Services;
using GacWarehouse.Core.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GacWarehouse.UnitTests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateNewOrder_ResultDataShouldNotBeNull()
        {            
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);

                var request = new OrderRequest
                {
                    CustomerId = 1,
                    OrderDetailsList = new List<OrderDetailsRequest>
                    {
                        new OrderDetailsRequest
                        {
                            ProductId = 1,
                            Quantity = 10
                        },
                        new OrderDetailsRequest
                        {
                            ProductId = 2,
                            Quantity = 20
                        }
                    }
                };

                //Act
                var response = service.CreateNewOrder(request).Result;

                //Assert
                Assert.True(response.Success);
                Assert.NotNull(response.Data);
            }
        }

        [Fact]
        public void CreateNewOrder_OrderQuantityShouldBeSubtractedFromProductTotalQuantity()
        {            
            //use context with data to run test
            using (var context = SetupAndGetInMemoryDbContext())
            {
                //Arrange
                var repository = new OrderRepository(context);
                var service = new OrderService(repository);
                
                
                var firstProductOrderDetail = new OrderDetailsRequest
                {
                    ProductId = 1,
                    Quantity = 10
                };
                var secondProductOrderDetail = new OrderDetailsRequest
                {
                    ProductId = 2,
                    Quantity = 20
                };

                var request = new OrderRequest
                {
                    CustomerId = 1,
                    OrderDetailsList = new List<OrderDetailsRequest>
                    {
                        firstProductOrderDetail,
                        secondProductOrderDetail
                    }
                };

                //Act

                //quantities before order
                var firstProductOriginalQuantity = repository.GetProductById(1).Quantity;
                var secondProductOriginalQuantity = repository.GetProductById(2).Quantity;

                var response = service.CreateNewOrder(request).Result;

                //quantities after order
                var firstProductCurrentQuantity = repository.GetProductById(1).Quantity;
                var secondProductCurrentQuantity = repository.GetProductById(2).Quantity;

                //Assert
                Assert.True(response.Success);
                Assert.Equal(firstProductOrderDetail.Quantity, firstProductOriginalQuantity - firstProductCurrentQuantity);
                Assert.Equal(secondProductOrderDetail.Quantity, secondProductOriginalQuantity - secondProductCurrentQuantity);
            }
        }

        private GacWarehouseDbContext SetupAndGetInMemoryDbContext()
        {
            //Create In-memory database
            var options = new DbContextOptionsBuilder<GacWarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: $"GacWarehouseInMemoryDb_{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            //Create Mocked Context and seed data 
            var context = new GacWarehouseDbContext(options);
            DbInitializer.Initialize(context);

            return context;
        }
    }
}

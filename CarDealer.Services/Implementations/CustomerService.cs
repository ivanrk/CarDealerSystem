namespace CarDealer.Services.Implementations
{
    using Models;
    using CarDealer.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using CarDealer.Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public CustomerModel ById(int id)
            => this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = this.db.Customers.Find(id);

            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver;

            this.db.SaveChanges();
        }

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            this.db.Add(customer);
            this.db.SaveChanges();
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery.OrderBy(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery.OrderByDescending(c => c.BirthDate).ThenBy(c => c.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return customersQuery
                .Select(c => new CustomerModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .ToList();
        }

        public CustomerTotalSalesModel TotalSales(int id)
            => this.db
                   .Customers
                   .Where(c => c.Id == id)
                   .Select(c => new CustomerTotalSalesModel
                   {
                       Name = c.Name,
                       IsYoungDriver = c.IsYoungDriver,
                       BoughtCars = c.Sales.Select(s => new SaleModel
                       {
                           Price = s.Car.Parts.Sum(p => p.Part.Price),
                           Discount = s.Discount
                       })
                       .ToList()
                   })
                   .FirstOrDefault();
    }
}

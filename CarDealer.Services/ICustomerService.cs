namespace CarDealer.Services
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);

        CustomerTotalSalesModel TotalSales(int id);

        void Create(string name, DateTime birthDate, bool isYoungDriver);

        CustomerModel ById(int id);

        void Edit(int id, string name, DateTime birthDate, bool isYoungDriver);
    }
}

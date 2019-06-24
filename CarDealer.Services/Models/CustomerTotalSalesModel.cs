namespace CarDealer.Services.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerTotalSalesModel
    {
        private const decimal AdditionalDiscount = 0.05m;

        public string Name { get; set; }

        public bool IsYoungDriver { get; set; }

        public IEnumerable<SaleModel> BoughtCars { get; set; }

        public decimal TotalMoneySpent
            => this.BoughtCars
                   .Sum(c => c.Price * (1 - (decimal)c.Discount))
                   * (IsYoungDriver ? 1 - AdditionalDiscount : 1);
    }
}

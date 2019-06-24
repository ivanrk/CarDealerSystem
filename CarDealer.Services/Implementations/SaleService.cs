namespace CarDealer.Services.Implementations
{
    using Models;
    using CarDealer.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleListModel> All()
            => this.db
                   .Sales
                   .Select(s => new SaleListModel
                   {
                       Id = s.Id,
                       CustomerName = s.Customer.Name,
                       IsYoungDriver = s.Customer.IsYoungDriver,
                       Price = s.Car.Parts.Sum(p => p.Part.Price),
                       Discount = s.Discount
                   })
                   .ToList();

        public SaleDetailsModel Details(int id)
            => this.db
                   .Sales
                   .Where(s => s.Id == id)
                   .Select(s => new SaleDetailsModel
                   {
                       CustomerName = s.Customer.Name,
                       IsYoungDriver = s.Customer.IsYoungDriver,
                       Car = new CarModel
                       {
                           Make = s.Car.Make,
                           Model = s.Car.Model,
                           TravelledDistance = s.Car.TravelledDistance
                       },
                       Price = s.Car.Parts.Sum(c => c.Part.Price),
                       Discount = s.Discount
                   })
                   .FirstOrDefault();
    }
}

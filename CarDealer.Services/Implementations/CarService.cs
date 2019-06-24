namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Data;
    using CarDealer.Data.Models;
    using CarDealer.Services.Models;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> All()
            => this.db
                .Cars.Select(c => new CarModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .OrderByDescending(c => c.Id)
                .ToList();

        public IEnumerable<CarModel> ByMake(string make)
            => this.db.Cars
                .Where(c => c.Make == make)
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        public IEnumerable<CarWithPartsModel> WithParts(int id)
        {
            return this.db.Cars
                .Where(c => c.Id == id)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                    .ToList()
                })
                .ToList();
        }

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> parts)
        {
            var existingPartIds = this.db
                .Parts
                .Where(p => parts.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partId in existingPartIds)
            {
                car.Parts.Add(new PartCar { PartId = partId });
            }

            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }
    }
}

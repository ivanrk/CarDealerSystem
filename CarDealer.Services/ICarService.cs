namespace CarDealer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> All();

        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> WithParts(int id);

        void Create(string make, string model, long travelledDistance, IEnumerable<int> parts);
    }
}

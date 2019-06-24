namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> AllListings(int page = 1, int pageSize = 10);

        IEnumerable<PartBasicModel> All();

        void Create(string name, decimal price, int quantity, int supplierId);

        void Edit(int id, decimal price, int quantity);

        void Delete(int id);

        PartListingModel ById(int id);

        int Total();
    }
}

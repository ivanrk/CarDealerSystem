namespace CarDealer.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleListModel> All();

        SaleDetailsModel Details(int id);
    }
}

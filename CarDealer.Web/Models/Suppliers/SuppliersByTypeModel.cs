namespace CarDealer.Web.Models.Suppliers
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public class SuppliersByTypeModel
    {
        public string Type { get; set; }

        public IEnumerable<SupplierListingModel> Suppliers { get; set; }
    }
}

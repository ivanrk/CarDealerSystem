namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult Local()
        {
            return View("Suppliers", this.GetSupplierModel(false));
        }

        public IActionResult Importers()
        {
            return View("Suppliers", this.GetSupplierModel(true));
        }

        private SuppliersByTypeModel GetSupplierModel(bool isImporter)
        {
            var type = isImporter ? "Importers" : "Local";

            return new SuppliersByTypeModel
            {
                Type = type,
                Suppliers = this.suppliers.AllListings(isImporter)
            };
        }
    }
}

namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        [Route("sales")]
        public IActionResult Sales()
            => View(this.sales.All());

        [Route("sales/{id}")]
        public IActionResult Details(int id)
            => View(this.sales.Details(id));
    }
}

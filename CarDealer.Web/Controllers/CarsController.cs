namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars, IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Authorize]
        [Route("cars/create")]
        public IActionResult Create()
            => base.View(new CarFormModel
            {
                AllParts = this.GetPartsSelectItems()
            });

        [Authorize]
        [HttpPost]
        [Route("cars/create")]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.AllParts = this.GetPartsSelectItems();
                return View(carModel);
            }

            this.cars.Create(carModel.Make, carModel.Model, carModel.TravelledDistance, carModel.SelectedParts);

            return RedirectToAction(nameof(All));
        }


        [Route("cars/all")]
        public IActionResult All()
            => View(this.cars.All());

        [Route("cars/{make}")]
        public IActionResult ByMake(string make)
        {
            var cars = this.cars.ByMake(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        [Route("cars/{id}/parts")]
        public IActionResult Parts(int id)
        {
            var cars = this.cars.WithParts(id);

            return View(cars);
        }

        private IEnumerable<SelectListItem> GetPartsSelectItems()
            => this.parts
                   .All()                
                   .Select(p => new SelectListItem
                   {
                       Text = p.Name,
                       Value = p.Id.ToString()
                   });
    }
}

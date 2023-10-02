using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using prog_avanzada_2_tp_integrador.Controllers.Services;
using prog_avanzada_2_tp_integrador.Models;
using prog_avanzada_2_tp_integrador.Models.ViewModels;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace prog_avanzada_2_tp_integrador.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MongoDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = new MongoDBContext(_configuration.GetConnectionString("mongodb"));
            
        }

        public IActionResult Index()
        {
            CarListView carList = new CarListView()
            {
                cars = _dbContext.Cars.Find(_ => true).ToList(),
                brands = _dbContext.Brands.Find(_ => true).ToList()
            };
            return View(carList);
        }

        public ActionResult CreateOrUpdate(CarView carView)
        {
            var carsCollection = _dbContext.Cars;
            if (carView.car.Id == null)
            {
                
                carsCollection.InsertOne(carView.car);
            }
            else
            {
                var filter = Builders<Car>.Filter.Eq(c => c.Id, carView.car.Id);
                carsCollection.ReplaceOne(filter, carView.car);
            }

            return RedirectToAction("Index");
        }

        //Mostrar formulario para creacion o modificacion de entrada
        public IActionResult CreateAndEdit(string CarId)
        {
            List<CarBrand> carBrands = _dbContext.Brands.Find(_ => true).ToList();
            CarView carView = new CarView()
            {
                car = new Car(),
                brands = carBrands.Select(brand => new SelectListItem()
                {
                    Text = brand.Name,
                    Value = brand.Id

                }).ToList()
            };

            if (CarId != null)
            {
                var filter = Builders<Car>.Filter.Eq(c => c.Id, CarId);
                List<Car> cars = _dbContext.Cars.Find(filter).ToList();
                carView.car = cars.First();
            }

            return View(carView);
        }

        public IActionResult Delete(string CarId)
        {
            var filter = Builders<Car>.Filter.Eq(c => c.Id, CarId);
            _dbContext.Cars.DeleteOne(filter);
            return RedirectToAction("Index"); // Redirige a la vista principal
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
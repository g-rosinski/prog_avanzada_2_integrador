using Microsoft.AspNetCore.Http;
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
    public class CarBrandController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MongoDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public CarBrandController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = new MongoDBContext(_configuration.GetConnectionString("mongodb"));

        }

        // GET: CarBrandController
        public ActionResult Index()
        {
            CarBrandListView carList = new CarBrandListView()
            {
                brands = _dbContext.Brands.Find(_ => true).ToList()
            };
            return View(carList);
        }

        //Mostrar formulario para creacion o modificacion de entrada
        public IActionResult CreateAndEdit(string CarBrandId)
        {
            CarBrandView carBrandView = new CarBrandView()
            {
                brand = new CarBrand() 
            };

            if (CarBrandId != null){
                var filter = Builders<CarBrand>.Filter.Eq(c => c.Id, CarBrandId);
                List<CarBrand> brands = _dbContext.Brands.Find(filter).ToList();
                carBrandView.brand = brands.First();
            }

            return View(carBrandView);
        }

        public ActionResult CreateOrUpdate(CarBrandView carBrandView)
        {
            var carsBrandCollection = _dbContext.Brands;
            if (carBrandView.brand.Id == null)
            {

                carsBrandCollection.InsertOne(carBrandView.brand);
            }
            else
            {
                var filter = Builders<CarBrand>.Filter.Eq(c => c.Id, carBrandView.brand.Id);
                carsBrandCollection.ReplaceOne(filter, carBrandView.brand);
            }

            return RedirectToAction("Index");
        }

        // GET: CarBrandController/Delete/5
        public ActionResult Delete(string CarBrandId)
        {
            var filter = Builders<CarBrand>.Filter.Eq(c => c.Id, CarBrandId);
            _dbContext.Brands.DeleteOne(filter);
            return RedirectToAction("Index"); // Redirige a la vista principal
        }
    }
}

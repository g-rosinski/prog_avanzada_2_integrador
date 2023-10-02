using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using prog_avanzada_2_tp_integrador.Models;

namespace prog_avanzada_2_tp_integrador.Controllers.Services
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(String connectionUrl)
        {
            var client = new MongoClient(connectionUrl);
            List<String> database = client.ListDatabaseNames().ToList();
            Console.WriteLine("Se pudo conectar correctamente a la Base de Datos: " + database.First().ToString());
            _database = client.GetDatabase(database.First().ToString());
        }

        public IMongoCollection<Car> Cars => _database.GetCollection<Car>("cars");

        public IMongoCollection<CarBrand> Brands => _database.GetCollection<CarBrand>("car_brands");
    }
}

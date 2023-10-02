using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace prog_avanzada_2_tp_integrador.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string? IdCarBrand { get; set; }
        public string? Model { get; set; }
        public string? Year { get; set; }
        public string? Color { get; set; }
        public string? Transmission { get; set; }
        public string? Engine { get; set; }

        public virtual CarBrand? CarBrand { get; set; }
    }
}

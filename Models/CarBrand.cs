using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace prog_avanzada_2_tp_integrador.Models
{
    public class CarBrand
    {
        public CarBrand()
        {
            Cars = new HashSet<Car>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? Name { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
    }
}

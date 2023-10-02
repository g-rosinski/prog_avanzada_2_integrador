using Microsoft.AspNetCore.Mvc.Rendering;

namespace prog_avanzada_2_tp_integrador.Models.ViewModels
{
    public class CarListView
    {
        public List<Car> cars { get; set; }
        public List<CarBrand> brands { get; set; }
    }
}

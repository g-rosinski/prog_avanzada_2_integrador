using Microsoft.AspNetCore.Mvc.Rendering;

namespace prog_avanzada_2_tp_integrador.Models.ViewModels
{
    public class CarView
    {
        public Car car {  get; set; }
        public List<SelectListItem> brands { get; set; }
    }
}

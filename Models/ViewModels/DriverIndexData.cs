using System.Security.Policy;
using CarAplication.Models;


namespace CarAplication.Models.ViewModels
{
    public class BrandIndexData
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}

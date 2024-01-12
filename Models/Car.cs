using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Security.Policy;

namespace CarAplication.Models
{
    public class Car
    {
        public int ID { get; set; }
        [Display(Name = "Model Name")]
        public string Name { get; set; }
        
        public decimal Price { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime AvailableDate { get; set; }
        
        public int? BrandID { get; set; }
        public  Brand? Brand { get; set; }

        


    }
}

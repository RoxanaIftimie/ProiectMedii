using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace CarAplication.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int? ClientID { get; set; }
        public Client? Client { get; set; }
        public int? CarID { get; set; }
        public Car? Car { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check In")]
        public DateTime PickupDate  { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Check Out")]
        public DateTime ReturnDate  { get; set; }

        [Display(Name = "Total Price")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }

}

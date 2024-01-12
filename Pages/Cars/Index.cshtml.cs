using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly CarAplication.Data.CarAplicationContext _context;

        public IndexModel(CarAplication.Data.CarAplicationContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get; set; } = default!;

        public int CarID { get; set; }
        public int CategoryID { get; set; }
        public string ModelSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            ModelSort = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            CurrentFilter = searchString;

            IQueryable<Car> carQuery = _context.Car
                .Include(b => b.Brand)
                
                    
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                carQuery = carQuery.Where(s => s.Name.Contains(searchString)
                    || s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "room_desc":
                    carQuery = carQuery.OrderByDescending(s => s.Name);
                    break;
                case "price_desc":
                    carQuery = carQuery.OrderByDescending(s => s.Price);
                    break;
                   
            }

            Car = await carQuery.ToListAsync();

            if (id != null)
            {
                CarID = id.Value;
                Car car = Car.FirstOrDefault(t => t.ID == id.Value);
                
            }
        }
    }
}

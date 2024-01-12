using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;
using CarAplication.Models.ViewModels;

namespace CarAplication.Pages.Brands
{
    public class IndexModel : PageModel
    {
        private readonly CarAplication.Data.CarAplicationContext _context;

        public IndexModel(CarAplication.Data.CarAplicationContext context)
        {
            _context = context;
        }

        public IList<Brand> Brand { get; set; } = default!;

        public BrandIndexData BrandData { get; set; }
        public int BrandID { get; set; }
        public int CarID { get; set; }

        public async Task OnGetAsync(int? id, int? bookID)
        {
            BrandData = new BrandIndexData();
            BrandData.Brands = await _context.Brand
            .Include(i => i.Cars)

            .OrderBy(i => i.BrandName)
            .ToListAsync();
            if (id != null)
            {
                BrandID = id.Value;
                Brand brand = BrandData.Brands
                .Where(i => i.ID == id.Value).Single();
                BrandData.Cars = brand.Cars;
            }
            //if (_context.Brand != null)
            //{
            //  Brand = await _context.Brand.ToListAsync();
            //}
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace CarAplication.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel

    {
        private readonly CarAplication.Data.CarAplicationContext _context;

        public CreateModel(CarAplication.Data.CarAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BrandID"] = new SelectList(_context.Set<Models.Brand>(), "ID", "BrandName");

            var car = new Car();
           

            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }


        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newCar = new Car();
            if (selectedCategories != null)
            {
                
            }
            
            _context.Car.Add(Car);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
       

    }
}

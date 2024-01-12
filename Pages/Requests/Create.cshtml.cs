using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly CarAplicationContext _context;

        public CreateModel(CarAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var carList = _context.Car
                .Include(b => b.Brand)
                .Select(x => new { x.ID, carFullName = x.Name + " - " + x.Brand.BrandName });
            ViewData["CarID"] = new SelectList(carList, "ID", "carFullName");
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");

            return Page();
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var car = await _context.Car.FindAsync(Request.CarID);
            if (car != null && Request.PickupDate  < car.AvailableDate)
            {
                ModelState.AddModelError("", "Check-in date must be after the car's available date.");
                return Page();
            }

            if (Request.ReturnDate  <= Request.PickupDate )
            {
                ModelState.AddModelError("", "Check-out date must be after the check-in date.");
                return Page();
            }

            // Calculate total price based on the number of days and car price
            var rentedDays = (int)(Request.ReturnDate  - Request.PickupDate ).TotalDays;
            Request.TotalPrice = rentedDays * car.Price;

            _context.Request.Add(Request);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

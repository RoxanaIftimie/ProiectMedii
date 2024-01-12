using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Cars
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly CarAplicationContext _context;

        public EditModel(CarAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Car = await _context.Car.FindAsync(id);

            if (Car == null)
            {
                return NotFound();
            }

            ViewData["BrandID"] = new SelectList(_context.Set<Brand>(), "ID", "BrandName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carToUpdate = await _context.Car.FindAsync(id);

            if (carToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Car>(
                carToUpdate,
                "Car",
                s => s.Name, s => s.Price, s => s.AvailableDate, s => s.BrandID))
            {
                // Additional logic for handling the picture update if necessary
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["BrandID"] = new SelectList(_context.Set<Brand>(), "ID", "BrandName", carToUpdate.BrandID);
            return Page();
        }
    }
}

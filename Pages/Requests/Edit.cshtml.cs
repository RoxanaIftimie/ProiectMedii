using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Requests
{
    public class EditModel : PageModel
    {
        private readonly CarAplication.Data.CarAplicationContext _context;

        public EditModel(CarAplication.Data.CarAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .Include(r => r.Client)
                .Include(r => r.Car)
                    .ThenInclude(c => c.Brand)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (request == null)
            {
                return NotFound();
            }

            Request = request;

            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");
            ViewData["CarID"] = new SelectList(_context.Car, "ID", "ID");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RequestExists(int id)
        {
          return _context.Request.Any(e => e.ID == id);
        }
    }
}

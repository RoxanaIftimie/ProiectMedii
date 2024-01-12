using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly CarAplicationContext _context;

        public IndexModel(CarAplicationContext context)
        {
            _context = context;
        }

        public IList<Request> Request { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Request != null)
            {
                Request = await _context.Request
                    .Include(r => r.Client)
                    .Include(r => r.Car)
                        .ThenInclude(f => f.Brand)
                    .ToListAsync();
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarAplication.Data;
using CarAplication.Models;

namespace CarAplication.Pages.Requests
{
    public class DetailsModel : PageModel
    {
        private readonly CarAplication.Data.CarAplicationContext _context;

        public DetailsModel(CarAplication.Data.CarAplicationContext context)
        {
            _context = context;
        }

      public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FirstOrDefaultAsync(m => m.ID == id);
            if (request == null)
            {
                return NotFound();
            }
            else 
            {
                Request = request;
            }
            return Page();
        }
    }
}

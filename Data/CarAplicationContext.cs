using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CarAplication.Models;

namespace CarAplication.Data
{
    public class CarAplicationContext : DbContext
    {
        public CarAplicationContext (DbContextOptions<CarAplicationContext> options)
            : base(options)
        {
        }

        public DbSet<CarAplication.Models.Car> Car { get; set; } = default!;

        public DbSet<CarAplication.Models.Brand> Brand { get; set; }

       

        public DbSet<CarAplication.Models.Client> Client { get; set; }

        public DbSet<CarAplication.Models.Request> Request { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vozilastiburek.Models;

namespace Vozilastiburek.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vozilastiburek.Models.Vozila> Vozila { get; set; } = default!;
    }
}

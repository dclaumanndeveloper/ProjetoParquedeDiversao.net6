using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ParquedeDiversoes.Models;

namespace ParquedeDiversoes.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ParquedeDiversoes.Models.Parque> Parque { get; set; }
        public DbSet<ParquedeDiversoes.Models.Brinquedo> Brinquedo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAPI.Data
{
    public class AnimalDbContext : DbContext
    {
        public AnimalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; } = null!;

        public DbSet<Address> Addresses { get; set; } = null!;

        public DbSet<Owner> Owners { get; set; } = null!;
    }
}
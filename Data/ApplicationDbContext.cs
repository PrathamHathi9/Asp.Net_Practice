// using System.Data.Entity;
using dotNet.Models;
using dotNet.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotNet.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<ProductDataModel> products { get; set; } 
    }
}

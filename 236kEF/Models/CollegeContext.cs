using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _236kEF.Models
{
    public class CollegeContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                ("Server=NEXTOUCH313\\SQLEXPRESS;Database=CollegeDB;Trusted_Connection=True;TrustServerCertificate=True;"); ;
        }

        public CollegeContext()
        {
            Database.EnsureCreated();
        }
    }
}

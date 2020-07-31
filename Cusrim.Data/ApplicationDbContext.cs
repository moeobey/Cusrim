using Cusrim.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cusrim.Data
{
    public class ApplicationDbContext: DbContext
    {
       
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> User { get; set; }

    }
}

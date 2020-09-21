using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMaking.Models;
using ProjectMaking.Models.EntityModel;

namespace ProjectMaking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Accounting> Accountings { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SalesMan> SalesMen { get; set; }
        public DbSet<Sehedule> Sehedules { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<WorkTask> ManagerTasks { get; set; }
    }
}

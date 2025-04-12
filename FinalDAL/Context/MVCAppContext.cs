using FinalDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDAL.Context
{
    public class MVCAppContext: IdentityDbContext<ApplicationUser>
    {
        public MVCAppContext(DbContextOptions<MVCAppContext>options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server=DESKTOP-U6FE2TE\\SQLEXPRESS;Database=CompanyMVC;Trusted_Connection=true;MultipleActiveResultSets=true");
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}

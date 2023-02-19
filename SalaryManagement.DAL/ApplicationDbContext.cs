//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalaryManage.Domain.Entity;

namespace SalaryManage.DAL
{
   public class ApplicationDbContext : DbContext /*IdentityDbContext<IdentityUser>*/
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      public DbSet<Employee> Employees { get; set; }
      public DbSet<PaymentRecord> PaymentRecords { get; set; }
      public DbSet<TaxYear> TaxYears { get; set; }

      //protected override void OnModelCreating(ModelBuilder builder)
      //{
      //   base.OnModelCreating(builder);
      //   // Customize the ASP.NET Identity model and override the defaults if needed.
      //   // For example, you can rename the ASP.NET Identity table names and more.
      //   // Add your customizations after calling base.OnModelCreating(builder);
      //}
   }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalaryManage.Domain.Entity;

namespace SalaryManage.Data
{
   public class ApplicationDbContext : IdentityDbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
      {
      }

      public DbSet<Employee> Employees { get; set; }

      //public DbSet<PaymentRecord> PaymentRecords { get; set; }

      public DbSet<TaxYear> TaxYears { get; set; }
   }
}
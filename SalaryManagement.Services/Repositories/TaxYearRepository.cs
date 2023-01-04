using SalaryManage.Data;
using SalaryManage.Domain.Entity;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class TaxYearRepository : Repository<TaxYear>, ITaxYearRepository
   {
      public TaxYearRepository(ApplicationDbContext context) : base(context)
      {
      }
   }
}

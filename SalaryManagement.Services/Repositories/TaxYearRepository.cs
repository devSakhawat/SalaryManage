using SalaryManage.DAL;
using SalaryManage.Domain.Entity;
using SalaryManage.Infrastructure.Constracts;

namespace SalaryManage.Infrastructure.Repositories
{
   public class TaxYearRepository : Repository<TaxYear>, ITaxYearRepository
   {
      public TaxYearRepository(ApplicationDbContext context) : base(context)
      {
      }

   }
}

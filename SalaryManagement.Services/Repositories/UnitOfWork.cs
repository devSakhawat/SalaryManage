using SalaryManage.Data;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      protected readonly ApplicationDbContext context;
      public UnitOfWork(ApplicationDbContext context)
      {
         this.context = context;
      }

      #region SaveChangesAsync
      public async Task<int> SaveChangesAsync()
      {
         return await context.SaveChangesAsync();
      }
      #endregion

      #region Employee
      private IEmployeeRepository employeeRepository;
      public IEmployeeRepository EmployeeRepository 
      {
         get 
         {
            if (employeeRepository == null)
               employeeRepository = new EmployeeRepository(context);

            return employeeRepository;
         }
      }
      #endregion
   }
}

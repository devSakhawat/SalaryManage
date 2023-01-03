using SalaryManage.Data;
using SalaryManage.Domain.Entity;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
   {
      public EmployeeRepository(ApplicationDbContext context) : base(context)
      {
      }

      public IEnumerable<Employee> GetEmployees()
      {
         return context.Employees.Where(e => e.IsDeleted == false);
      }

      public decimal StudentLoadnRepaymentAmout(int Id, decimal totalAmount)
      {
         throw new NotImplementedException();
      }

      public decimal UnionFees(int Id)
      {
         throw new NotImplementedException();
      }
   }
}

using SalaryManage.Domain.Entity;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface IEmployeeRepository : IRepository<Employee>
   {
       IEnumerable<Employee> GetEmployees();
      decimal StudentLoadnRepaymentAmout(int Id, decimal totalAmount);
      decimal UnionFees(int Id);
   }
}

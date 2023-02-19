using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryManage.Domain.Entity;

namespace SalaryManage.Infrastructure.Constracts
{
   public interface IEmployeeRepository : IRepository<Employee>
   {
      IEnumerable<Employee> GetEmployees();
      decimal StudentLoanRepaymentAmout(int id, decimal totalAmount);
      decimal UnionFees(int id);
      IEnumerable<SelectListItem> GetAllEmployeesForPayCompute();
   }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryManage.Domain.Entity;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface IEmployeeRepository : IRepository<Employee>
   {
      IEnumerable<Employee> GetEmployees();
      decimal StudentLoadnRepaymentAmout(int id, decimal totalAmount);
      decimal UnionFees(int id);
      IEnumerable<SelectListItem> GetAllEmployeesForPayCompute();
   }
}

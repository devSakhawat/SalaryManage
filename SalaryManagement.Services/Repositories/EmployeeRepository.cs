using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryManage.Data;
using SalaryManage.Domain.Entity;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
   {
      private decimal studentLoanAmount;

      public EmployeeRepository(ApplicationDbContext context) : base(context)
      {
      }

      public IEnumerable<Employee> GetEmployees()
      {
         return context.Employees.Where(e => e.IsDeleted == false);
      }

      public decimal StudentLoadnRepaymentAmout(int id, decimal totalAmount)
      {
        var employee = GetById(id);
         if (employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000 )
         {
            studentLoanAmount = 15m;
         }
         else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
         {
            studentLoanAmount = 38m;
         }
         else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
         {
            studentLoanAmount = 60m;
         }
         else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
         {
            studentLoanAmount = 83m;
         }
         else
         {
            studentLoanAmount = 0m;
         }
         return studentLoanAmount;
      }

      public decimal UnionFees(int id)
      {
         var employee = GetById(id);
         var fee = employee.UnionMember == UnionMember.Yes ? 10m : 0m;
         return fee;
      }

      public IEnumerable<SelectListItem> GetAllEmployeesForPayCompute()
      {
         return GetEmployees().Select(emp => new SelectListItem()
         {
            Text = emp.FullName,
            Value = emp.Id.ToString()
         });
      }
   }
}

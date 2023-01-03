using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryManage.Domain.Entity;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface IPayComputeRepository : IRepository<PaymentRecord>
   {
      IEnumerable<SelectListItem> GetAllTaxYear();
      decimal OverTimeHours(decimal hoursWorked, decimal contractualHours);      
      decimal OverTimeRate(decimal hourlyRate);
      decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHours);
      decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate);
      decimal ToatalEarnings(decimal overtimeEarnigns, decimal contractualEarnings);
      decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees);
      decimal NetPay(decimal totalEarnings, decimal totalDeduction);
   }
}

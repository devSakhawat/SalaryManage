using Microsoft.AspNetCore.Mvc.Rendering;
using SalaryManage.Data;
using SalaryManage.Domain.Entity;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class PayComputeRepository : Repository<PaymentRecord>, IPayComputeRepository
   {
      private decimal contractualEarnings;
      private decimal overtimeHours;
      public PayComputeRepository(ApplicationDbContext context) : base(context)
      {
      }

      //Get TaxYear By Id
      public TaxYear GetTaxYearById(int id)
         => context.TaxYears.Where(year => year.Id == id).FirstOrDefault();

      //AllTaxYear
      public IEnumerable<SelectListItem> GetAllTaxYear()
      {
         var allTaxYear = context.TaxYears.Select(taxYears => new SelectListItem 
         { 
            Text = taxYears.YearOfTax,
            Value = taxYears.Id.ToString()
         });
         return allTaxYear;
      }

      // OverTimeHours calculation = Total(hoursWorked) - Total(contractualHours)
      public decimal OverTimeHours(decimal hoursWorked, decimal contractualHours)
      {
         if (hoursWorked <= contractualHours)
         {
            overtimeHours = 0.0m;
         }
         else if (hoursWorked > contractualHours)
         {
            overtimeHours = hoursWorked - contractualHours;
         }
         return overtimeHours;
      }

      // OverTimeRate is 1.5 time multiplication of contractual(HourlyRate)
      public decimal OverTimeRate(decimal hourlyRate)
      {
         return hourlyRate * 1.5m;
      }

      // OverTimeEarning Calculation
      public decimal OverTimeEarnings(decimal overtimeRate, decimal overtimeHours)
      {
         return overtimeHours * overtimeRate;
      }

      // Contractual Earning Calculation
      public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
      {
         if (hoursWorked < contractualHours)
         {
            contractualEarnings = hoursWorked * hourlyRate;
         }
         else
         {
            contractualEarnings = contractualHours * hourlyRate;
         }
         return contractualEarnings;
      }

      //Total Earning of an employee with overtime.
      public decimal TotalEarnings(decimal overtimeEarnigns, decimal contractualEarnings)
      {
         return overtimeEarnigns + contractualEarnings;
      }

      // Total Deduction Calculation
      public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
      {
         return tax + nic + studentLoanRepayment + unionFees;
      }

      // NetPay calculation
      public decimal NetPay(decimal totalEarnings, decimal totalDeduction)
      {
         return totalEarnings - totalDeduction;
      }
   }
}

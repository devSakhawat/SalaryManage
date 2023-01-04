using SalaryManage.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace SalaryManagement.Domain.ViewModel
{
   public class PaymentRecordIndex
   {
      public int Id { get; set; }
      public int EmployeeId { get; set; }
      [Display(Name = "Name")]
      public string FullName { get; set; }
      [Display(Name = "Pay Date")]
      public DateTime PayDate { get; set; }
      [Display(Name = "Month")]
      public string PayMonth { get; set; }
      public int TaxYearId { get; set; }
      public string Year { get; set; }
      [Display(Name = "Total Earning")]
      public decimal TotalEarning { get; set; }
      [Display(Name = "Total Deduction")]
      public decimal TotalDeduction { get; set; }
      [Display(Name = "Net Payment")]
      public decimal NetPayment { get; set; }


      public Employee Employee { get; set; }
   }
}

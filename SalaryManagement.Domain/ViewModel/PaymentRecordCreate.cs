using SalaryManage.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace SalaryManage.Domain.ViewModel
{
   public class PaymentRecordCreate
   {
      public int Id { get; set; }
      public int EmployeeId { get; set; }
      //public Employee Employee { get; set; }
      //[MaxLength(100)]
      //public string FullName { get; set; }
      //public string NiNo { get; set; }
      [Display(Name = "Pay Date"), DataType(DataType.Date)]
      public DateTime PayDate { get; set; } = DateTime.UtcNow;
      [Display(Name = "Pay Month")]
      public string PayMonth { get; set; } = DateTime.Today.Month.ToString();
      [Display(Name = "Tax Year")]
      public int TaxYearId { get; set; }
      //public TaxYear TaxYear { get; set; }
      public string? TaxCode { get; set; } = "1250L";
      [Display(Name = "Hourly Rate")]
      public decimal HourlyRate { get; set; }
      [Display(Name = "Hours Worked")]
      public decimal HoursWorked { get; set; }
      [Display(Name = "Contractual Hours")]
      public decimal ContractualHours { get; set; } = 208m;
      [Display(Name = "Overtime Hours")]
      public decimal OvertimeHours { get; set; }
      [Display(Name = "Contractual Earnings")]
      public decimal ContractualEarnings { get; set; }
      public decimal OvertimeEarnings { get; set; }
      public decimal Tax { get; set; }

      // NIC = National Insurance Contribution
      public decimal NIC { get; set; }
      //public decimal? UnionFee { get; set; }

      // SLC = Student Load Company
      //public decimal? SLC { get; set; }
      public decimal TotalEarnings { get; set; }
      public decimal TotalDeduction { get; set; }
      public decimal NetPayment { get; set; }
   }
}

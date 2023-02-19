using SalaryManage.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace SalaryManage.Domain.ViewModel
{
   public class TaxYearCreate
   {
      public int Id { get; set; }
      [Display(Name = "Year Of Tax")]
      //[RegularExpression(@"^[0-9]{4, 4}[/\]{1}[0-9]{4,4}")]
      public string YearOfTax { get; set; }
   }
}

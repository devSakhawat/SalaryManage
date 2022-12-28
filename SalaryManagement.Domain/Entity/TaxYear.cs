namespace SalaryManage.Domain.Entity
{
   public class TaxYear : BaseModel
   {
      public int Id { get; set; }
      public string YearOfTax { get; set; }
   }
}
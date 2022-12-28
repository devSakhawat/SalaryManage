namespace SalaryManage.Domain.Entity
{
   public class BaseModel
   {
      public int CreateBy { get; set; }
      public DateTime CreatedDate { get; set; }
      public int ModifiedBy { get; set; }
      public DateTime ModifiedDate { get; set; }

      public bool IsDeleted { get; set; }
   }
}

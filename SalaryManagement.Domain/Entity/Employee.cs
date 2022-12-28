using System.ComponentModel.DataAnnotations;

namespace SalaryManage.Domain.Entity
{
   public class Employee : BaseModel
   {
      public int Id { get; set; }

      [Required]
      public string EmployeeNo { get; set; }

      [Required, StringLength(50)]
      public string FirstName { get; set; }

      [StringLength(50)]
      public string MiddleName { get; set; }

      [Required, StringLength(50)]
      public string LastName { get; set; }

      public string FullName { get; set; }

      public string Gender { get; set; }

      public string ImageUrl { get; set; }

      public DateTime DOB { get; set; }

      public DateTime DateJoined { get; set; }

      public string Phone { get; set; }

      public string Designation { get; set; }

      public string Email { get; set; }

      [Required, MaxLength(50)]
      public string NationalInsuranceNo { get; set; }

      public PaymentMethod PaymentMethod { get; set; }

      public StudentLoan StudentLoan { get; set; }

      public UnionMember UnionMember { get; set; }

      [Required, MaxLength(50)]
      public string Address { get; set; }

      [Required, MaxLength(50)]
      public string City { get; set; }

      [Required, MaxLength(50)]
      public string Passcode { get; set; }

      public IEnumerable<PaymentRecord> PaymentRecords { get; set; }
   }
}

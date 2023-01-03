using Microsoft.AspNetCore.Http;
using SalaryManage.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace SalaryManage.Domain.ViewModel
{
   public class EmployeeCreate : BaseModel
   {
      public int Id { get; set; }

      [Required(ErrorMessage = "Employee Number is required"),
          RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
      // ReqularExpression start ^ and end $
      // start with a to z minimum 3 and maximum 3 than 0 to 9 must be 3 characters
      public string EmployeeNo { get; set; }

      [Required(ErrorMessage = "First Name is required"), StringLength(50, MinimumLength = 2)]
      [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
      public string FirstName { get; set; }

      [StringLength(50), Display(Name = "Middle Name")]
      public string MiddleName { get; set; }

      [Required(ErrorMessage = "Last Name is required"), StringLength(50, MinimumLength = 2)]
      [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Last Name")]
      public string LastName { get; set; }

      public string FullName
      {
         get
         {
            return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ". ").ToUpper()) + LastName;
         }
      }

      public string Gender { get; set; }

      [Display(Name = "Photo")]
      public IFormFile ImageUrl { get; set; }

      [DataType(DataType.Date), Display(Name = "Date Of Birth")]
      public DateTime DOB { get; set; } = DateTime.UtcNow;

      [DataType(DataType.Date), Display(Name = "Date Joined")]
      public DateTime DateJoined { get; set; } = DateTime.UtcNow;

      public string Phone { get; set; }

      [Required(ErrorMessage = "Job Role is required"), StringLength(100)]
      public string Designation { get; set; }

      [DataType(DataType.EmailAddress)]
      public string Email { get; set; }
      //SSN 000-00-0000 @"^\d{3}-\d{2}-\d{4}$"
      // usa national security no: any degit length 3, any degit length 2, any degit length 4
      [Required, StringLength(50), Display(Name = "NI No.")]
      [RegularExpression(@"^[A-CEGHJ-PR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[A-D\s]$")]
      public string NationalInsuranceNo { get; set; }

      [Display(Name = "Payment Method")]
      public PaymentMethod PaymentMethod { get; set; }

      [Display(Name = "Student Loan")]
      public StudentLoan StudentLoan { get; set; }
      [Display(Name = "Union Member")]
      public UnionMember UnionMember { get; set; }

      [Required, StringLength(150)]
      public string Address { get; set; }

      [Required, StringLength(50)]
      public string City { get; set; }

      [Required, StringLength(50)]
      public string Postcode { get; set; }
   }
}

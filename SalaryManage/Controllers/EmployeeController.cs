using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualBasic;
using SalaryManage.Domain.Entity;
using SalaryManage.Domain.ViewModel;
using SalaryManagement.Infrastructure.Constracts;
using SalaryManagement.Infrastructure.Repositories;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace SalaryManage.Controllers
{
   public class EmployeeController : Controller
   {
      private readonly IUnitOfWork context;
      private readonly HostingEnvironment hosting;
      public EmployeeController(IUnitOfWork context, HostingEnvironment hosting)
      {
         this.context = context;
         this.hosting = hosting;
      }

      #region Add Employee
      // Add record to table
      [HttpGet]
      public IActionResult Create()
      {
         var model = new EmployeeCreate();
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(EmployeeCreate model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               var employee = new Employee
               {
                  Id = model.Id,
                  EmployeeNo = model.EmployeeNo,
                  FirstName = model.FirstName,
                  MiddleName = model.MiddleName,
                  LastName = model.LastName,
                  FullName = model.FullName,
                  Gender = model.Gender,
                  Email = model.Email,
                  DOB = model.DOB,
                  DateJoined = model.DateJoined,
                  NationalInsuranceNo = model.NationalInsuranceNo,
                  PaymentMethod = model.PaymentMethod,
                  StudentLoan = model.StudentLoan,
                  UnionMember = model.UnionMember,
                  Address = model.Address,
                  City = model.City,
                  Phone = model.Phone,
                  Designation = model.Designation
               };
               if (model.ImageUrl != null && model.ImageUrl.Length > 0)
               {
                  var uploadDir = @"images/employee";
                  if (!Directory.Exists(uploadDir))
                     Directory.CreateDirectory(uploadDir);

                  var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                  var extension = Path.GetExtension(model.ImageUrl.FileName);
                  fileName = DateTime.UtcNow.ToString("F") + fileName + extension;
                  var filePath = Path.Combine(hosting.ContentRootPath, uploadDir, fileName);
                  await model.ImageUrl.CopyToAsync(new FileStream(filePath, FileMode.Create));
                  employee.ImageUrl = "/" + uploadDir + "/" + fileName;
               }
            }
            catch (Exception)
            {
               throw;
            }
         }
         return View();
      }
      #endregion

      #region Index
      public IActionResult Index()
      {
         var employees = context.EmployeeRepository.GetEmployees().Select(
            employee => new EmployeeIndex
            { 
               Id = employee.Id,
               EmployeeNo= employee.EmployeeNo,
               FullName= employee.FullName,
               Gender = employee.Gender,
               Designation = employee.Designation,
               City= employee.City,
               DateJoined= employee.DateJoined,
               ImageUrl= employee.ImageUrl
            }).ToList();
         return View(employees);
      }
      #endregion

      #region Edit
      [HttpGet]
      public async Task<IActionResult> Edit(int Id)
      {
         var employee = await context.EmployeeRepository.FirstOrDefaultAsync(e => e.Id == Id && e.IsDeleted == false);
         if (employee == null)
            return NotFound();

         var model = new EmployeeEdit
         {
            Id = employee.Id,
            EmployeeNo = employee.EmployeeNo,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            LastName = employee.LastName,            
            DOB = employee.DOB,
            Gender = employee.Gender,
            Phone = employee.Phone,
            Email = employee.Email,
            DateJoined = employee.DateJoined,
            Designation = employee.Designation,
            NationalInsuranceNo = employee.NationalInsuranceNo,
            PaymentMethod = employee.PaymentMethod,
            StudentLoan = employee.StudentLoan,
            UnionMember = employee.UnionMember,
            Address = employee.Address,
            City = employee.City,
            Postcode = employee.Passcode
         };

         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Editd(EmployeeEdit model)
      {
         if (ModelState.IsValid)
         {
            try
            {
               var employee = await context.EmployeeRepository.FirstOrDefaultAsync(e => e.Id == model.Id && e.IsDeleted == false);
               if (employee == null)
                  return NotFound();
               employee.EmployeeNo = model.EmployeeNo;
               employee.FirstName = model.FirstName;
               employee.MiddleName = model.MiddleName;
               employee.LastName = model.LastName;     
               employee.DOB = model.DOB;
               employee.Gender = model.Gender;
               employee.Phone = model.Phone;
               employee.Email = model.Email;
               employee.DateJoined = model.DateJoined;
               employee.Designation = model.Designation;
               employee.NationalInsuranceNo = model.NationalInsuranceNo;
               employee.PaymentMethod = model.PaymentMethod;
               employee.StudentLoan = model.StudentLoan;
               employee.UnionMember = model.UnionMember;
               employee.Address = model.Address;
               employee.City = model.City;
               employee.Passcode = model.Postcode;

               if (employee.ImageUrl != null && employee.ImageUrl.Length > 0)
               {
                  var uploadDir = @"images/employee";
                  if (!Directory.Exists(uploadDir))
                     Directory.CreateDirectory(uploadDir);

                  var fileName = Path.GetFileNameWithoutExtension(model.ImageUrl.FileName);
                  var extentsion = Path.GetExtension(model.ImageUrl.FileName);
                  fileName = DateTime.UtcNow.ToString("F")+ fileName + extentsion.ToLower();

                  var contentPath = Path.Combine(hosting.ContentRootPath, uploadDir, fileName);
                  await model.ImageUrl.CopyToAsync(new FileStream(contentPath, FileMode.Create));
                  employee.ImageUrl = "/" + uploadDir + "/" + fileName;
               }
               context.EmployeeRepository.Update(employee);
               await context.SaveChangesAsync();
               return RedirectToAction("Index");
            }
            catch (Exception)
            {
               throw;
            }
         }
         return View(model);
      }
      #endregion edit

      #region Detail
      [HttpGet]
      public async Task<IActionResult> Detail(int Id)
      {
         try
         {
            var employee = await context.EmployeeRepository.FirstOrDefaultAsync(e => e.Id == Id);
            if (employee == null)
               return NotFound();

            EmployeeDetail employeeDetail = new EmployeeDetail
            {
               Id = employee.Id,
               EmployeeNo = employee.EmployeeNo,
               FullName = employee.FullName,
               Gender = employee.Gender,
               DOB = employee.DOB,
               DateJoined = employee.DateJoined,
               Designation = employee.Designation,
               NationalInsuranceNo = employee.NationalInsuranceNo,
               Phone = employee.Phone,
               Email = employee.Email,
               PaymentMethod = employee.PaymentMethod,
               StudentLoan = employee.StudentLoan,
               UnionMember = employee.UnionMember,
               Address = employee.Address,
               City = employee.City,
               Passcode = employee.Passcode,
               ImageUrl = employee.ImageUrl
            };
            return View(employeeDetail);
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion Employee Detail

      #region Employee Delete
      [HttpGet]
      public async Task<IActionResult> Delete(int Id)
      {
         try
         {
            var employee = await context.EmployeeRepository.FirstOrDefaultAsync(e => e.Id == Id && e.IsDeleted == false);

            if (employee == null)
               return NotFound();

            EmployeeDelete employeeDelete = new EmployeeDelete
            {
               Id = employee.Id,
               FullName = employee.FullName
            };
            return View(employeeDelete);
         }
         catch (Exception)
         {
            throw;
         }
      }
      
      [HttpDelete]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Delete(EmployeeDelete model)
      {
         var employee = await context.EmployeeRepository.FirstOrDefaultAsync(e => e.Id == model.Id && e.IsDeleted == false);
         if (employee == null)
            return NotFound();
         // soft delete
         employee.IsDeleted = true;

         //// hard delete
         //context.EmployeeRepository.Delete(employee);
         await context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }
      #endregion employee delete
   }
}

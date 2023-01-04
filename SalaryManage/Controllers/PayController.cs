using Microsoft.AspNetCore.Mvc;
using SalaryManage.Domain.Entity;
using SalaryManagement.Domain.ViewModel;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManage.Controllers
{
   public class PayController : Controller
   {
      private readonly IUnitOfWork context;
      private decimal overtimeHrs;
      private decimal overtimeErns;
      private decimal contractualErns;
      private decimal totalErns;
      private decimal tax;
      private decimal unionFee;
      private decimal studentLoan;
      private decimal nationalInsurance;
      private decimal totalDeduction;

      public PayController(IUnitOfWork context)
      {
         this.context = context;
      }

      #region PaymentRecordCreate
      [HttpGet]
      public IActionResult Create()
      {
         ViewBag.employees = context.EmployeeRepository.GetAllEmployeesForPayCompute();
         ViewBag.taxYears = context.PayComputeRepository.GetAllTaxYear();
         var model = new PaymentRecordCreate();
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(PaymentRecordCreate model)
      {
         if (ModelState.IsValid) 
         {
            var payRecord = new PaymentRecord()
            {
               Id = model.Id,
               EmployeeId = model.EmployeeId,
               FullName = context.EmployeeRepository.GetById(model.EmployeeId).FirstName,
               NiNo = context.EmployeeRepository.GetById(model.EmployeeId).NationalInsuranceNo,
               PayDate = model.PayDate,
               PayMonth = model.PayMonth,
               TaxYearId = model.TaxYearId,
               TaxCode = model.TaxCode,
               HourlyRate = model.HourlyRate,
               HoursWorked = model.HoursWorked,
               ContractualHours = model.ContractualHours,
               OvertimeHours = overtimeHrs = context.PayComputeRepository.OverTimeHours(model.HoursWorked, model.ContractualHours),
               ContractualEarnings = contractualErns = context.PayComputeRepository.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
               OvertimeEarnings = overtimeErns = context.PayComputeRepository.OverTimeEarnings(context.PayComputeRepository.OverTimeRate(model.HourlyRate), overtimeHrs),
               TotalEarnings = totalErns = context.PayComputeRepository.TotalEarnings(contractualErns, overtimeErns),
               Tax = tax = context.TaxRepository.TaxAmount(totalErns),
               UnionFee = unionFee = context.EmployeeRepository.UnionFees(model.EmployeeId),
               SLC = studentLoan = context.EmployeeRepository.StudentLoadnRepaymentAmout(model.EmployeeId, totalErns),
               NIC = nationalInsurance = context.NationalInsuranceContributionRepository.NiContribution(totalErns),
               TotalDeduction = totalDeduction = context.PayComputeRepository.TotalDeduction(tax, nationalInsurance, studentLoan, unionFee),
               NetPayment = context.PayComputeRepository.NetPay(totalErns, totalDeduction)
            };
            context.PayComputeRepository.Add(payRecord);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewBag.employees = context.EmployeeRepository.GetAllEmployeesForPayCompute();
         ViewBag.taxYears = context.PayComputeRepository.GetAllTaxYear();
         return View();
      }
      #endregion

      #region PaymentRecordIndex
      public IActionResult Index()
      {
         var payRecord = context.PayComputeRepository.GetAll().Select(async pay => new PaymentRecordIndex 
         { 
            Id= pay.Id,
            EmployeeId = pay.EmployeeId,
            FullName = pay.FullName,
            PayDate= pay.PayDate,
            PayMonth= pay.PayMonth,
            TaxYearId = pay.TaxYearId,
            Year = context.PayComputeRepository.GetTaxYearById(pay.TaxYearId).YearOfTax,
            TotalEarning = pay.TotalEarnings,
            TotalDeduction = pay.TotalDeduction,
            NetPayment = pay.NetPayment,
            Employee = pay.Employee
         });

         return View(payRecord);
      }
      #endregion

      #region PaymentRecordDetail
      [HttpGet]
      public IActionResult Detail(int id)
      {
         var paymentRecord = context.PayComputeRepository.GetById(id);
         if (paymentRecord == null)
            return NotFound();

         var model = new PaymentRecordDetail
         {
            Id = paymentRecord.Id,
            EmployeeId = paymentRecord.EmployeeId,
            FullName = paymentRecord.FullName,
            NiNo = paymentRecord.NiNo,
            PayDate = paymentRecord.PayDate,
            PayMonth = paymentRecord.PayMonth,
            TaxYearId = paymentRecord.TaxYearId,
            Year = context.PayComputeRepository.GetTaxYearById(paymentRecord.Id).YearOfTax,
            TaxCode = paymentRecord.TaxCode,
            HourlyRate = paymentRecord.HourlyRate,
            HoursWorked = paymentRecord.HourlyRate,
            ContractualHours = paymentRecord.ContractualHours,
            OvertimeHours = paymentRecord.OvertimeHours,
            OvertimeRate = context.PayComputeRepository.OverTimeRate(paymentRecord.HourlyRate),
            ContractualEarnings = paymentRecord.ContractualEarnings,
            Tax = paymentRecord.Tax,
            NIC = paymentRecord.NIC,
            SLC = paymentRecord.SLC,
            UnionFee = paymentRecord.UnionFee,
            TotalEarnings = paymentRecord.TotalEarnings,
            TotalDeduction = paymentRecord.TotalDeduction,
            Employee = paymentRecord.Employee,
            TaxYear = paymentRecord.TaxYear,
            NetPayment = paymentRecord.NetPayment
         };
         return View(model);
      }
      #endregion

      #region Payslip
      [HttpGet]
      public IActionResult Payslip(int id)
      {
         var paymentRecord = context.PayComputeRepository.GetById(id);
         if (paymentRecord == null)
            return NotFound();

         var model = new PaymentRecordDetail()
         {
            Id = paymentRecord.Id,
            EmployeeId = paymentRecord.EmployeeId,
            FullName = paymentRecord.FullName,
            NiNo = paymentRecord.NiNo,
            PayDate = paymentRecord.PayDate,
            PayMonth = paymentRecord.PayMonth,
            TaxYearId = paymentRecord.TaxYearId,
            Year = context.PayComputeRepository.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
            TaxCode = paymentRecord.TaxCode,
            HourlyRate = paymentRecord.HourlyRate,
            HoursWorked = paymentRecord.HoursWorked,
            ContractualHours = paymentRecord.ContractualHours,
            OvertimeHours = paymentRecord.OvertimeHours,
            OvertimeRate = context.PayComputeRepository.OverTimeRate(paymentRecord.HourlyRate),
            ContractualEarnings = paymentRecord.ContractualEarnings,
            OvertimeEarnings = paymentRecord.OvertimeEarnings,
            Tax = paymentRecord.Tax,
            NIC = paymentRecord.NIC,
            UnionFee = paymentRecord.UnionFee,
            SLC = paymentRecord.SLC,
            TotalEarnings = paymentRecord.TotalEarnings,
            TotalDeduction = paymentRecord.TotalDeduction,
            Employee = paymentRecord.Employee,
            TaxYear = paymentRecord.TaxYear,
            NetPayment = paymentRecord.NetPayment
         };

         return View();
      }
      #endregion




   }
}
using System.Web;
using Microsoft.AspNetCore.Mvc;
using SalaryManage.Domain.Entity;
using SalaryManage.Domain.ViewModel;
using SalaryManage.Infrastructure.Constracts;

namespace SalaryManage.Controllers
{
   public class PayController : Controller
   {
      private readonly IUnitOfWork context;
      private decimal overtimeHours;
      private decimal overtimeEarnings;
      private decimal contractualEarnings;
      private decimal totalEarnings;
      private decimal tax;
      private decimal unionFee;
      private decimal studentLoan;
      private decimal nationalInsurance;
      private decimal totalDeduction;

      public PayController(IUnitOfWork context)
      {
         this.context = context;
      }

      #region Payment RecordCreate
      [HttpGet]
      public IActionResult Create()
      {
         ViewBag.employees = context.EmployeeRepository.GetAllEmployeesForPayCompute();
         ViewBag.taxYears = context.PayComputeRepository.GetAllTaxYear();
         var model = new PaymentRecordCreate();
         return View(model);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(PaymentRecordCreate model)
      {         
         if (ModelState.IsValid)
         {
            var paymentRecord = new PaymentRecord()
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
               OvertimeHours = overtimeHours = context.PayComputeRepository.OverTimeHours(model.HoursWorked, model.ContractualHours),
               ContractualEarnings = contractualEarnings = context.PayComputeRepository.ContractualEarnings(model.ContractualHours, model.HoursWorked, model.HourlyRate),
               OvertimeEarnings = overtimeEarnings = context.PayComputeRepository.OverTimeEarnings(context.PayComputeRepository.OverTimeRate(model.HourlyRate), overtimeHours),
               TotalEarnings = totalEarnings = context.PayComputeRepository.TotalEarnings(contractualEarnings, overtimeEarnings),
               Tax = tax = context.TaxRepository.TaxAmount(totalEarnings),
               UnionFee = unionFee = context.EmployeeRepository.UnionFees(model.EmployeeId),
               SLC = studentLoan = context.EmployeeRepository.StudentLoanRepaymentAmout(model.EmployeeId, totalEarnings),
               NIC = nationalInsurance = context.NationalInsuranceContributionRepository.NiContribution(totalEarnings),
               TotalDeduction = totalDeduction = context.PayComputeRepository.TotalDeduction(tax, nationalInsurance, studentLoan, unionFee),
               NetPayment = context.PayComputeRepository.NetPay(totalEarnings, totalDeduction),
               CreatedDate = DateTime.UtcNow
            };

            context.PayComputeRepository.Add(paymentRecord);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         ViewBag.employees = context.EmployeeRepository.GetAllEmployeesForPayCompute();
         ViewBag.taxYears = context.PayComputeRepository.GetAllTaxYear();
         return View(model);
      }
      #endregion

      #region Payment Record Index
      public IActionResult Index()
      {
         var payRecord = context.PayComputeRepository.GetPaymentRecords().Select(pay => new PaymentRecordIndex 
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

      #region Payment Record Detail
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

         return View(model);
      }
      #endregion

      #region GeneratePdf
      //public async Task<IActionResult> GeneratePayslipPdf(int id)
      //{
      //   var paymentRecord = await context.PayComputeRepository.FirstOrDefaultAsync(p => p.Id == id);
      //   var payslip = new ActionAsPdf("Payslip", new { id = id })
      //   {
      //      FileName = paymentRecord.NiNo + ".pdf"
      //   };
      //   return payslip;
      //}
      #endregion
   }
}
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SalaryManage.Domain.Entity;
using SalaryManage.Domain.ViewModel;
using SalaryManage.Infrastructure.Constracts;

namespace SalaryManage.Controllers
{
   public class TaxYearController : Controller
   {
      private IUnitOfWork context;
      public TaxYearController(IUnitOfWork context)
      {
         this.context = context;
      }

      #region Tax Year Create
      [HttpGet]
      public IActionResult Create()
      {
         TaxYearCreate taxYear = new TaxYearCreate();
         return View(taxYear);
      }

      public async Task<IActionResult> Create(TaxYearCreate taxYearCreate)
      {
         if (ModelState.IsValid)
         {
            try
            {
               var taxYear = new TaxYear
               {
                  Id = taxYearCreate.Id,
                  YearOfTax = taxYearCreate.YearOfTax
               };
               context.TaxYearRepository.Add(taxYear);
               await context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
               throw;
            }
         }
         return View(taxYearCreate);
      }
      #endregion

      #region Tax Year Index
      public async Task<IActionResult> Index()
      {
         var taxYears = await context.TaxYearRepository.QueryAsync(ty => ty.IsDeleted == false);
         return View(taxYears);
      }
      #endregion

      #region Tax Year Update
      [HttpGet]
      public async Task<IActionResult> Edit(int id)
      {
         var taxYear = await context.TaxYearRepository.FirstOrDefaultAsync(ty => ty.Id == id);
         if (taxYear == null)
            return NotFound();

         return View(taxYear);
      }

      [HttpPost]
      public async Task<IActionResult> Edit(TaxYearCreate model)
      {
         if (ModelState.IsValid)
         {
            var taxYear = new TaxYear
            {
               Id = model.Id,
               YearOfTax = model.YearOfTax
            };
            if (taxYear == null)
               return BadRequest();

            context.TaxYearRepository.Update(taxYear);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
         }
         return View(model);
      }
      #endregion

      #region Tax Year Delete
      [HttpGet]
      public async Task<IActionResult> Delete(int id)
      {
         try
         {
            var taxYear = await context.TaxYearRepository.FirstOrDefaultAsync(ty => ty.Id == id);
            if (taxYear == null)
               return NotFound();

            return View(taxYear);
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpPost]
      public async Task<IActionResult> Delete(TaxYear model)
      {
         try
         {
            var taxYear = await context.TaxYearRepository.FirstOrDefaultAsync(ty => ty.Id == model.Id);
            taxYear.IsDeleted = true;
            context.TaxYearRepository.Update(taxYear);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         catch (Exception)
         {
            throw;
         }
      }
      #endregion
   }
}

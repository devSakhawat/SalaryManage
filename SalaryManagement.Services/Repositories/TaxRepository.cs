using SalaryManage.Data;
using SalaryManagement.Infrastructure.Constracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class TaxRepository : ITaxRepository
   {
      private decimal taxRate;
      private decimal taxAmount;
      private readonly ApplicationDbContext context;
      public TaxRepository(ApplicationDbContext context)
      {
         this.context = context;
      }

      public decimal TaxAmount(decimal totalAmount)
      {
         
         if (totalAmount <= 1042)
         {
            // Tax Free Rate up to 1042
            taxRate = 0.0m;
            taxAmount = totalAmount * taxRate;
         }
         else if (totalAmount > 1042 && totalAmount <= 3125)
         {
            // Basic tax rate
            taxRate = 0.20m;
            // Income tax
            taxAmount = (1042 * .0m) + ((totalAmount - 1042) * 0.20m);
         }
         else if (totalAmount > 3125 && totalAmount <= 12500)
         {
            // Higher tax rate
            taxRate = 0.40m;
            //Income Tax
            taxAmount = (1042 * 0.0m) + ((3125 - 1042) * .20m) + ((totalAmount - 3125) * .40m);
         }
         else if (totalAmount > 12500)
         {
            // Additional tax reate
            taxRate = 0.45m;
            //Income tax
            taxAmount = (1042 * 0.0m) + ((3125 - 1042) * 0.20m) + ((12500 - 3125) * 0.40m) + ((totalAmount - 12500) * 0.45m);
         }
         return taxAmount;
      }
   }
}

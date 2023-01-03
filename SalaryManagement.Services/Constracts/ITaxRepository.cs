using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface ITaxRepository
   {
      decimal TaxAmount(decimal totalAmount);
   }
}

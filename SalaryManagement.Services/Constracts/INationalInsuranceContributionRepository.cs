using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface INationalInsuranceContributionRepository
   {
      decimal NiContribution(decimal totalAmount);
   }
}

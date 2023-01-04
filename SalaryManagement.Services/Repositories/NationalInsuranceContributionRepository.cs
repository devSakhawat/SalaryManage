using SalaryManage.Data;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class NationalInsuranceContributionRepository : INationalInsuranceContributionRepository
   {
      private readonly ApplicationDbContext context;
      private decimal NIRate;
      private decimal NIC;
      public NationalInsuranceContributionRepository(ApplicationDbContext context)
      {
         this.context = context;
      }

      public decimal NiContribution(decimal totalAmount)
      {
         if (totalAmount < 719)
         {
            // Lower earning limit rate & below primary threshold
            NIRate = 0.0m;
            NIC = 0m;
         }
         else if (totalAmount >= 719 && totalAmount <= 4127)
         {
            // Between primary threshold and upper earnings limit (UEL)
            NIRate = 0.12m;
            NIC = ((totalAmount - 719) * NIRate);
         }
         else if (totalAmount > 4167)
         {
            // Above Upper Earnings
            NIRate = 0.02m;
            NIC = ((4167 - 719) * 0.12m) + ((totalAmount - 4167));
         }
         return NIC;
      }
   }
}

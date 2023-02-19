namespace SalaryManage.Infrastructure.Constracts
{
   public interface INationalInsuranceContributionRepository
   {
      decimal NiContribution(decimal totalAmount);
   }
}

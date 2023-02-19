namespace SalaryManage.Infrastructure.Constracts
{
   public interface IUnitOfWork
   {
      Task<int> SaveChangesAsync();

      IEmployeeRepository EmployeeRepository { get; }

      IPayComputeRepository PayComputeRepository { get; }

      ITaxYearRepository TaxYearRepository { get; }

      ITaxRepository TaxRepository { get; }

      INationalInsuranceContributionRepository NationalInsuranceContributionRepository { get; }
   }
}

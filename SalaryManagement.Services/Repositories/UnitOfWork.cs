using SalaryManage.Data;
using SalaryManagement.Infrastructure.Constracts;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class UnitOfWork : IUnitOfWork
   {
      protected readonly ApplicationDbContext context;
      public UnitOfWork(ApplicationDbContext context)
      {
         this.context = context;
      }

      #region SaveChangesAsync
      public async Task<int> SaveChangesAsync()
      {
         return await context.SaveChangesAsync();
      }
      #endregion

      #region Employee
      private IEmployeeRepository employeeRepository;
      public IEmployeeRepository EmployeeRepository
      {
         get
         {
            if (employeeRepository == null)
               employeeRepository = new EmployeeRepository(context);

            return employeeRepository;
         }
      }
      #endregion

      #region PaymentCompute
      private IPayComputeRepository payComputeRepository;
      public IPayComputeRepository PayComputeRepository
      {
         get
         {
            if (payComputeRepository == null)
               payComputeRepository = new PayComputeRepository(context);

            return payComputeRepository;
         }
      }
      #endregion

      #region TaxYear
      private ITaxYearRepository taxYearRepository;

      public ITaxYearRepository TaxYearRepository
      {
         get
         {
            if (taxYearRepository == null)
               taxYearRepository = new TaxYearRepository(context);

            return taxYearRepository;
         }
      }
      #endregion

      #region Tax
      private ITaxRepository taxRepository;
      public ITaxRepository TaxRepository
      {
         get
         {
            if (taxRepository == null)
               taxRepository = new TaxRepository(context);

            return TaxRepository;
         }
      }
      #endregion

      #region NationalInsuranceContribution
      private INationalInsuranceContributionRepository nationalInsuranceContributionRepository;
      public INationalInsuranceContributionRepository NationalInsuranceContributionRepository
      {
         get
         { 
            if(nationalInsuranceContributionRepository == null)
               nationalInsuranceContributionRepository = new NationalInsuranceContributionRepository(context);

            return nationalInsuranceContributionRepository;
         }
      }
      #endregion
   }
}

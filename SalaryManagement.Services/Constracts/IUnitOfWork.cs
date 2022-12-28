namespace SalaryManagement.Infrastructure.Constracts
{
   public interface IUnitOfWork
   {
      Task<int> SaveChangesAsync();

      IEmployeeRepository EmployeeRepository { get; }
   }
}

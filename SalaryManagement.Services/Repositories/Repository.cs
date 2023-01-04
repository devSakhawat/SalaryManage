using Microsoft.EntityFrameworkCore;
using SalaryManage.Data;
using SalaryManagement.Infrastructure.Constracts;
using System.Linq.Expressions;

namespace SalaryManagement.Infrastructure.Repositories
{
   public class Repository<T> : IRepository<T> where T : class
   {
      protected readonly ApplicationDbContext context;
      public Repository(ApplicationDbContext context)
      {
         this.context = context;
      }
      #region Add
      public T Add(T entity)
      {
         return context.Set<T>().Add(entity).Entity;
      }
      #endregion

      #region FirstOrDefaultAsync
      public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
      {
         return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
      }
      #endregion

      #region GetById
      public T GetById(int id)
      {
         return context.Set<T>().Find(id);
      }
      #endregion

      #region GetAll
      public IEnumerable<T> GetAll()
      {
         return context.Set<T>().ToList();
      }
      #endregion

      #region QueryAsync
      public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate)
      {
         return await context.Set<T>().AsQueryable().AsNoTracking().Where(predicate).ToListAsync();
      }
      #endregion

      #region Update
      public void Update(T entity)
      {
         context.Entry(entity).State = EntityState.Modified;
         context.Set<T>().Update(entity);
      }
      #endregion

      #region Delete
      public void Delete(T entity)
      {
         context.Entry(entity).State = EntityState.Modified;
         context.Set<T>().Remove(entity);
      }
      #endregion
   }
}

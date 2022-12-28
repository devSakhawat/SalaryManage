﻿using System.Linq.Expressions;

namespace SalaryManagement.Infrastructure.Constracts
{
   public interface IRepository<T>
   {
      // add record to table
      T Add(T entity);

      // get a record by perameter match
      Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

      // get all record of table
      Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

      // update record
      void Update(T entity);

      // delete a reocrd
      void Delete(T entity);
   }
}
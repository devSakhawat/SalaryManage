﻿namespace SalaryManage
{
   public class Pagination<T> : List<T>
   {
      public int PageIndex { get; private set; }
      public int TotalPages { get; private set; }
      public Pagination(List<T> items, int count, int pageIndex, int pageSize)
      {
         PageIndex = pageIndex;
         TotalPages = (int)Math.Ceiling(count / (double)pageSize);
         this.AddRange(items);
      }

      // Enable and disable button
      public bool IsPreviousPageAvailable => PageIndex > 1;
      public bool IsNextPageAvailable => PageIndex < TotalPages;

      public static Pagination<T> Create(IList<T> source, int pageIndex, int pageSize)
      {
         var count = source.Count();
         var items = source.Skip((pageIndex - 1 ) * pageSize).Take(pageSize).ToList();
         return new Pagination<T>(items, count, pageIndex, pageSize);
      }
   }
}

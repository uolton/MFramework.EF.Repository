using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MFramework.EF.Repository
{
    /// <summary>
    /// Read repository
    /// </summary>
    public interface IReadRepository : IDisposable
    {
        /// <summary>
        /// Select data from database using a where clause
        /// </summary>
        /// <typeparam name="TItem">Type of data to select</typeparam>
        /// <param name="whereClause">Where clause / function</param>
        /// <param name="orderBy">Order by clause</param>
        /// <param name="skip">Paging implementation = number of records to skip</param>
        /// <param name="top">Paging implementation = number of records to return</param>
        /// <param name="include">Navigation properties to include</param>
        /// <returns>Items selected</returns>
        IEnumerable<TItem> Select<TItem>(
          Expression<Func<TItem, bool>> whereClause = null,
          IOrderByClause<TItem>[] orderBy = null,
          int skip = 0,
          int top = 0,
          string[] include = null)
          where TItem : class, new();
    }
}

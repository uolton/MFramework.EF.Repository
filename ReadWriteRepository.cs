using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MFramework.Common.Core.Collections.Extensions;
using MFramework.EF.Core;


namespace MFramework.EF.Repository
{
    /// <summary>
    /// Read-write repository
    /// </summary>
    /// <typeparam name="TContext">Type of DbContext to implement actions on</typeparam>
    public abstract class ReadWriteRepository<TContext> : WriteRepository<TContext>, IReadWriteRepository
        where TContext : ExtendedDbContext
    {
        private TContext _context;

        private void CreateContext()
        {
            if (_context == null)
            {
                _context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { ConnectionString });
            }
        }

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
        public IEnumerable<TItem> Select<TItem>(
             Expression<Func<TItem, bool>> whereClause = null,
             IOrderByClause<TItem>[] orderBy = null,
             int skip = 0,
             int top = 0,
             string[] include = null)
             where TItem : class, new()
        {
            CreateContext();
            IQueryable<TItem> data = _context.Set<TItem>();
            // handle where
            if (whereClause != null)
            {
                data = data.Where(whereClause);
            }

            //handle order by
            if (orderBy != null)
            {
                bool isFirstSort = true;
                orderBy.ToList().ForEach(one =>
                {
                    data = one.ApplySort(data, isFirstSort);
                    isFirstSort = false;
                });
            }

            // handle paging
            if (skip > 0)
            {
                data = data.Skip(skip);
            }
            if (top > 0)
            {
                data = data.Take(top);
            }

            //handle includes
            if (include != null)
            {
                include.ForEach(one => data = data.Include(one));
            }

            foreach (var item in data)
            {
                yield return item;
            }
        }
    }
}

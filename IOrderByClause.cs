using System.Linq;

namespace MFramework.EF.Repository
{
    /// <summary>
    /// Order by clause interface
    /// Used to sort the data
    /// </summary>
    /// <typeparam name="T">Type of DbSet to sort</typeparam>
    public interface IOrderByClause<T>
        where T : class, new()
    {
        /// <summary>
        /// Apply sort to IQueryable or IOrderedQueryable
        /// </summary>
        /// <param name="query">Query to sort</param>
        /// <param name="firstSort">Is this the very first sort</param>
        /// <returns></returns>
        IOrderedQueryable<T> ApplySort(IQueryable<T> query, bool firstSort);
    }
}

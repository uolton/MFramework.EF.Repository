using System;
using System.Data.Entity;
using MFramework.EF.Core;

namespace MFramework.EF.Repository
{
    /// <summary>
    /// Write repository
    /// </summary>
    /// <typeparam name="TContext">Type of DbContext to implement actions on</typeparam>
    public abstract class WriteRepository<TContext> : IWriteRepository
        where TContext : ExtendedDbContext
    {
        private TContext _context;

        /// <summary>
        /// Connection string for DbContext
        /// </summary>
        protected abstract string ConnectionString { get; }

        private void CreateContext()
        {
            if (_context == null)
            {
                _context = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { ConnectionString });
            }
        }

        /// <summary>
        /// Insert new item into database
        /// </summary>
        /// <typeparam name="TItem">Type of item to insert</typeparam>
        /// <param name="item">Item to insert</param>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <returns>Inserted item</returns>
        public TItem Insert<TItem>(TItem item, bool saveImmediately = true)
            where TItem : class, new()
        {
            CreateContext();
            var set = _context.Set<TItem>();
            set.Add(item);
            if (saveImmediately)
            {
                _context.SaveChanges();
            }
            return item;
        }

        /// <summary>
        /// Update an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to update</typeparam>
        /// <param name="item">Item to update</param>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <returns>Updated item</returns>
        public TItem Update<TItem>(TItem item, bool saveImmediately = true)
            where TItem : class, new()
        {
            CreateContext();
            var set = _context.Set<TItem>();
            var entry = _context.Entry(item);
            if (entry != null && entry.State != EntityState.Detached)
            {
                // entity is already in memory
                entry.State = EntityState.Modified;
            }
            else
            {
                set.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }

            if (saveImmediately)
            {
                _context.SaveChanges();
            }
            return item;
        }

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to delete</typeparam>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <param name="item">Item to delete</param>
        public void Delete<TItem>(TItem item, bool saveImmediately = true)
           where TItem : class, new()
        {
            CreateContext();
            var set = _context.Set<TItem>();
            var entry = _context.Entry(item);
            if (entry != null && entry.State != EntityState.Detached)
            {
                // entity is already in memory
                entry.State = EntityState.Deleted;
            }
            else
            {
                set.Attach(item);
                _context.Entry(item).State = EntityState.Deleted;
            }

            if (saveImmediately)
            {
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Save all pending changes
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Dispose context
        /// </summary>
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}

using System;

namespace MFramework.EF.Repository
{
    /// <summary>
    /// Writable repository
    /// </summary>
    public interface IWriteRepository : IDisposable
    {
        /// <summary>
        /// Update an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to update</typeparam>
        /// <param name="item">Item to update</param>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <returns>Updated item</returns>
        TItem Update<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();

        /// <summary>
        /// Delete an item
        /// </summary>
        /// <typeparam name="TItem">Type of item to delete</typeparam>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <param name="item">Item to delete</param>
        void Delete<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();


        /// <summary>
        /// Insert new item into database
        /// </summary>
        /// <typeparam name="TItem">Type of item to insert</typeparam>
        /// <param name="item">Item to insert</param>
        /// <param name="saveImmediately">If set to true, saved occurs right away</param>
        /// <returns>Inserted item</returns>
        TItem Insert<TItem>(TItem item, bool saveImmediately = true) where TItem : class, new();

        /// <summary>
        /// Save all pending changes
        /// </summary>
        void Save();
    }
}

using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TickTest.Services
{
    public interface IStorageManager<K>
    {
        /// <summary>
        /// Get a storage property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        T Get<T>(Expression<Func<K, T>> selector);

        /// <summary>
        /// Set a storage property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> Set<T>(Expression<Func<K, T>> selector, T value);
    }
}
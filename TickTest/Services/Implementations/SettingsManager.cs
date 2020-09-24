using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace TickTest.Services
{
    public class SettingsManager<K> : IStorageManager<K>
    {
        public T Get<T>(Expression<Func<K, T>> selector)
        {
            var settings = new Storage<K>();
            var result = settings.FirstOrDefault();
            if (result == null)
            {
                result = (K)Activator.CreateInstance(typeof(K));
                settings.Add(result);
                settings.SaveChanges();
            }

            try
            {
                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
                var obj = prop.GetValue(result, null);
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch { }
            return default;
        }

        public async Task<bool> Set<T>(Expression<Func<K, T>> selector, T value)
        {
            var settings = new Storage<K>();
            var result = settings.FirstOrDefault();
            bool isnew = false;
            if (result == null)
            {
                result = (K)Activator.CreateInstance(typeof(K));
                isnew = true;
            }
            try
            {
                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
                prop.SetValue(result, value, null);
                if (isnew) settings.Add(result);
                await settings.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

    }
}
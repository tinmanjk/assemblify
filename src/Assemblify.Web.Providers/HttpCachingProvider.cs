using Assemblify.Common;
using Assemblify.Web.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Assemblify.Web.Providers
{
    public class HttpCachingProvider: ICachingProvider
    {
        private static readonly object LockObject = new object();

        public T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds)
        {
            if (HttpRuntime.Cache[itemName] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[itemName] == null)
                    {
                        var data = getDataFunc();
                        HttpRuntime.Cache.Insert(
                            itemName,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[itemName];
        }

        // not tested
        public T GetFromCache<T>(string itemName)
        {
            return (T)HttpRuntime.Cache[itemName];
        }

        public ICollection<string> GetUserNames()
        {
            return this.GetFromCache<ICollection<string>>(GlobalConstants.CachingUserNames);
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }
    }
}

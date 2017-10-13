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

        // overload without duration
        public T GetOrAdd<T>(string cacheId, Func<T> getDataCallBack)
        {
            if (HttpRuntime.Cache[cacheId] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[cacheId] == null)
                    {
                        var data = getDataCallBack();
                        HttpRuntime.Cache.Insert(
                            cacheId,
                            data,
                            null,
                            Cache.NoAbsoluteExpiration,
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[cacheId];
        }

        public T GetOrAdd<T>(string cachedId, Func<T> getDataCallBack, int durationInSeconds)
        {

            if (HttpRuntime.Cache[cachedId] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[cachedId] == null)
                    {
                        var data = getDataCallBack();
                        HttpRuntime.Cache.Insert(
                            cachedId,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)HttpRuntime.Cache[cachedId];
        }

        public void Remove(string itemName)
        {
            HttpRuntime.Cache.Remove(itemName);
        }
    }
}

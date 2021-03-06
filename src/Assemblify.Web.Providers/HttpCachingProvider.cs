﻿using Assemblify.Common;
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
    public class HttpCachingProvider: IHttpCachingProvider
    {
        private static readonly object LockObject = new object();
        private readonly IHttpContextProvider httpContextProvider;

        public HttpCachingProvider(IHttpContextProvider httpContextProvider)
        {
            if (httpContextProvider == null)
            {
                throw new ArgumentNullException(nameof(httpContextProvider));
            }

            this.httpContextProvider = httpContextProvider;
        }

        public T GetOrAdd<T>(string cacheId, Func<T> getDataCallBack, int durationInSeconds = GlobalConstants.CachingDefaultDuration)
        {
            if (httpContextProvider.ContextCache[cacheId] == null)
            {
                lock (LockObject)
                {
                    if (httpContextProvider.ContextCache[cacheId] == null)
                    {
                        var data = getDataCallBack();
                        httpContextProvider.ContextCache.Insert(
                            cacheId,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                    }
                }
            }

            return (T)httpContextProvider.ContextCache[cacheId];
        }
    }
}

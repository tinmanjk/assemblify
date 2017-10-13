using Assemblify.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Contracts
{
    public interface IHttpCachingProvider
    {
        T GetOrAdd<T>(string cacheId, Func<T> getDataFunc, int durationInSeconds);
        T GetOrAdd<T>(string cacheId, Func<T> getDataCallBack);

        void Update<T>(string cacheId, Func<T> getDataCallBack);
        

        void Remove(string cacheId);
    }
}

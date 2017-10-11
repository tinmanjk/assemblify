using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Contracts
{
    public interface ICachingProvider
    {
        T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds);

        void Remove(string itemName);
    }
}

﻿using Assemblify.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Web.Providers.Contracts
{
    public interface ICachingProvider
    {
        T GetOrAdd<T>(string cacheId, Func<T> getDataFunc, int durationInSeconds);
        T GetOrAdd<T>(string cacheId, Func<T> getDataCallBack);


        //ICollection<string> GetUserNames();

        void Remove(string itemName);
    }
}
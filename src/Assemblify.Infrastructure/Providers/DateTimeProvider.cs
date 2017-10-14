﻿using Assemblify.Infrastructure.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Infrastructure.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.UtcNow;
        }

        public DateTime GetTimeFromCurrentTime(int hours, int minutes, int seconds)
        {
            var timeSpan = new TimeSpan(hours, minutes, seconds);

            return DateTime.UtcNow.Add(timeSpan);
        }
    }
}

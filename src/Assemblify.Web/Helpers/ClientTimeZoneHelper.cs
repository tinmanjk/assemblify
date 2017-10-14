using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assemblify.Web.Helpers
{
    public class ClientTimeZoneHelper
    {
        public static string ConvertToLocalTimeAndFormat(DateTime? dt, string format)
        {
            if (dt == null)
            {
                return "";

            }
            var dtNonNull = (DateTime)dt;

            var o = HttpContext.Current.Session["tzo"];

            var tzo = o == null ? 0 : Convert.ToDouble(o);

            dtNonNull = dtNonNull.AddMinutes(-1 * tzo);

            var s = dtNonNull.ToString(format);

            if (tzo == 0)
                s += " GMT";

            return s;
        }
    }
}
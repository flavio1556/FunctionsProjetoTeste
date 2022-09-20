using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionsAPP.Ultis
{
    public static class Ultis
    {
        public static string GetLastStringURL(this string Url)
        {
            string[] vs = Url.Split('/');
            return vs.LastOrDefault();
        }
    }
}

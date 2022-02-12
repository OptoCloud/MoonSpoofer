using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoonSpoofer
{
    internal static class IPTools
    {
        public static String GetMyIP()
        {
            using (WebClient wc = new WebClient())
            using (StringReader reader = new StringReader(wc.DownloadString("http://checkip.amazonaws.com/")))
            return reader.ReadToEnd();
        }

    }
}

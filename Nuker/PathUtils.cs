using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonNuker
{
    internal static class PathUtils
    {
        public static String AppdataLocalPath { get{ return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData); } }
        public static String AppdataLocalLowPath { get { return AppdataLocalPath + "Low"; } }
        public static String AppdataRoamingPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); } }

        public static String DefaultVRCXPath { get { return AppdataRoamingPath + "\\VRCX"; } }
    }
}

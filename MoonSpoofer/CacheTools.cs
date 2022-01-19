using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// \AppData\LocalLow\VRChat
// \AppData\Local\Temp\VRChat
// \AppData\Local\Temp\DefaultCompany
// \AppData\Roaming\VRCX

namespace MoonSpoofer
{
    internal static class CacheTools
    {
        private static void DeleteIfExists(String path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
        public static void NukeVRChat()
        {
            DeleteIfExists(PathUtils.AppdataLocalLowPath + "\\VRChat");
            DeleteIfExists(PathUtils.AppdataLocalPath + "\\Temp\\VRChat");
            DeleteIfExists(PathUtils.AppdataLocalPath + "\\Temp\\DefaultCompany");
        }
        public static void NukeVRCX()
        {
            DeleteIfExists(PathUtils.AppdataRoamingPath + "\\VRCX");
        }
    }
}

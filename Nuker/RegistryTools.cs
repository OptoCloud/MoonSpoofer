using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MoonNuker
{
    internal static class RegistryTools
    {
        public static void NukeVRChat()
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("SOFTWARE");
            softwareKey.DeleteSubKeyTree("VRChat");

            RegistryKey unityTechKey = softwareKey.OpenSubKey("Unity Technologies");
            foreach (String valueName in unityTechKey.GetValueNames().Where(name => name.Contains("VRC")))
                unityTechKey.DeleteValue(valueName);
        }
        public static void NukeVRCX()
        {
        }
    }
}

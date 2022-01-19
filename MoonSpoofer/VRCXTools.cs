using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MoonSpoofer
{
    internal static class VRCXTools
    {
        private static bool IsInstalled()
        {
            return Directory.Exists(PathUtils.DefaultVRCXPath);
        }

        public static void NukeVRCX()
        {
            foreach (var process in Process.GetProcessesByName("VRCX"))
                while (!process.HasExited) { process.Kill(); Thread.Sleep(1000); }

            if (IsInstalled())
            {
                DialogResult dialogResult = MessageBox.Show($"Do you want to clean all of its VRCX's caches?", $"Clean VRCX?", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    CacheTools.NukeVRCX();
                    RegistryTools.NukeVRCX();
                }
                else if (dialogResult != DialogResult.No)
                    throw new Exception("Unexpected input.");
            }
        }
    }
}

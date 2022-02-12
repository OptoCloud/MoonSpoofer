using MelonLoader;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MoonSpoofer
{
	public class MoonSpoofer : MelonMod
	{
		private static MelonPreferences_Category MoonSpoofCategory = null;
		private static MelonPreferences_Entry<String> MoonSpoofEntryHWID = null;
		private static void InitPrefrences()
		{
			if (MoonSpoofCategory != null) return;

			MoonSpoofCategory = MelonPreferences.CreateCategory("MoonSpoofer", "Moon Spoofer");
			MoonSpoofEntryHWID = MoonSpoofCategory.CreateEntry("HWID", String.Empty, is_hidden: true);
		}

		public static String OriginalHWID { get; } = UnityEngine.SystemInfo.deviceUniqueIdentifier;
		public static String SpoofedHWID
		{
			get
			{
				InitPrefrences();
				String value = MoonSpoofEntryHWID.Value;
				while (value == MoonSpoofer.OriginalHWID || value.Length != OriginalHWID.Length)
					value = HWIDTools.GenerateFakeHWID(OriginalHWID.Length);
				SpoofedHWID = value;
				return value;
			}
			private set
			{
				InitPrefrences();
				MoonSpoofEntryHWID.Value = value;
				MoonSpoofCategory.SaveToFile(false);

                RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("SOFTWARE");

                RegistryKey melonKey;
                if (!softwareKey.GetSubKeyNames().Contains("MelonLoader"))
                    melonKey = softwareKey.CreateSubKey("MelonLoader");
                else
                    melonKey = softwareKey.OpenSubKey("MelonLoader");

                RegistryKey spooferKey;
                if (melonKey.GetSubKeyNames().Contains("MoonSpoofer"))
                    spooferKey = melonKey.CreateSubKey("MoonSpoofer");
                else
                    spooferKey = melonKey.OpenSubKey("MoonSpoofer");

                spooferKey.SetValue("HWID", value, RegistryValueKind.String);
            }
		}

		public unsafe override void OnApplicationStart()
		{
            try
            {
                if (!File.Exists("MoonSpoofer/Nuker.exe"))
                {
                    Directory.CreateDirectory("MoonSpoofer");
                    File.WriteAllBytes("MoonSpoofer/Nuker.exe", Resource.NukerAssembly);
                }

                DialogResult dialogResult = MessageBox.Show("Click yes if you want to clean everything and perform a clean boot\nGame will close if it fails at performing all required actions.", "Clean everything?", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    var proc = Process.Start("MoonSpoofer/MoonNuker.exe");
                    proc.WaitForExit();
                    if (proc.ExitCode != 0)
                    {
                        LoggerInstance.Error($"Failed to nuke: {proc.StandardOutput.ReadToEnd()}");
                        HardKillApplication();
                    }    
                }
                else if (dialogResult != DialogResult.No)
                {
                    HardKillApplication();
                }

                if (!Patches.Init(HarmonyInstance, LoggerInstance))
					HardKillApplication();

				if (UnityEngine.SystemInfo.deviceUniqueIdentifier != MoonSpoofer.SpoofedHWID)
				{
					LoggerInstance.Error("Failed to spoof: Spoofed HWID is not expected value.");
					HardKillApplication();
				}

                LoggerInstance.Msg("Patched HWID; below two should NOT match:");
                LoggerInstance.Msg("System:  " + MoonSpoofer.OriginalHWID);
				LoggerInstance.Msg("Spoofed: " + UnityEngine.SystemInfo.deviceUniqueIdentifier);
            }
			catch (Exception ex)
			{
				LoggerInstance.Error($"Exception occured during patch: {ex}");
				HardKillApplication();
			}
		}

		private void HardKillApplication()
		{
			LoggerInstance.Msg("Killing game...");
			while (true) { Process.GetCurrentProcess().Kill(); }
		}
	}
}

using MelonLoader;
using System;
using System.Diagnostics;
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

			}
		}

		public unsafe override void OnApplicationStart()
		{
            try
            {
                DialogResult dialogResult = MessageBox.Show("Click yes if you want to clean everything and perform a clean boot\nGame will close if it fails at performing all required actions.", "Clean everything?", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    CacheTools.NukeVRChat();
                    RegistryTools.NukeVRChat();
                    VRCXTools.NukeVRCX();
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

				LoggerInstance.Msg("Patched HWID; below two should match:");
				LoggerInstance.Msg("System:  " + MoonSpoofer.SpoofedHWID);
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

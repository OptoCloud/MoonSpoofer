namespace ExampleNamespace
{
	public static partial class ExampleClass
	{
		private static string cachedHWID = null;
		public static string ExampleGetterMethod()
		{
            if (cachedHWID != null)
            {
                return cachedHWID;
            }

            RegistryKey moonSpooferKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\MelonLoader\\MoonSpoofer", true);
            if (moonSpooferKey == null)
            {
                moonSpooferKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\MelonLoader\\MoonSpoofer", true);
            }
            else
            {
                try
                {
                    cachedHWID = (string)moonSpooferKey.GetValue("HWID");
                }
                catch (Exception)
                {
                }
            }

            int hwidlen = UnityEngine.SystemInfo.deviceUniqueIdentifier.Length;
            if (cachedHWID == null || cachedHWID.Length != hwidlen)
            {
                System.Random random = new System.Random(Environment.TickCount);
                byte[] array = new byte[hwidlen / 2 + 1];
                random.NextBytes(array);
                cachedHWID = BitConverter.ToString(array).Replace("-", string.Empty).ToLower().Substring(0, hwidlen);

                moonSpooferKey.SetValue("HWID", cachedHWID, RegistryValueKind.String);
            }

            return cachedHWID;
		}
	}
}

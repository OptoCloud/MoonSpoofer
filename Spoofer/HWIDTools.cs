using System;

namespace MoonSpoofer
{
	internal static class HWIDTools
	{
		public static String GenerateFakeHWID(int len)
		{
			Random random = new Random(Environment.TickCount);
			byte[] data = new byte[(len / 2) + 1];
			random.NextBytes(data);
			return BitConverter.ToString(data).Replace("-", string.Empty).ToLower().Substring(0, len);
		}
	}
}

using MelonLoader;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnhollowerBaseLib;

namespace MoonSpoofer
{
	internal static class Patches
	{
		private static UnityEngine.Object SpoofedHwidOj;

		public static unsafe bool Init(HarmonyLib.Harmony harmonyInstance, MelonLogger.Instance loggerInstance)
		{
			SpoofedHwidOj = new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(MoonSpoofer.SpoofedHWID));

            String iCallName = "UnityEngine.SystemInfo::GetDeviceUniqueIdentifier";
            IntPtr iCallAddress = IL2CPP.il2cpp_resolve_icall(iCallName);
			if (iCallAddress == IntPtr.Zero)
			{
				loggerInstance.Error("Failed to patch: Can't resolve the icall.");
				return false;
			}

			MelonUtils.NativeHookAttach((IntPtr)(&iCallAddress), typeof(Patches).GetMethod("GetDeviceIdPatch", BindingFlags.Static | BindingFlags.NonPublic).MethodHandle.GetFunctionPointer());

			return true;
		}

		[MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
		private static IntPtr GetDeviceIdPatch()
		{
			return SpoofedHwidOj.Pointer;
		}
	}
}

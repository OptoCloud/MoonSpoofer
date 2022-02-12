using System;

namespace MoonNuker
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                CacheTools.NukeVRChat();
                RegistryTools.NukeVRChat();
                VRCXTools.NukeVRCX();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }

            return 0;
        }
    }
}

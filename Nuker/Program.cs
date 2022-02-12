using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoonSpoofer
{
    internal class IPMap
    {
        class IpInfo
        {
            public string IP { get; set; }
            public string HWID { get; set; }
            public string UserID { get; set; }
            public string VPN_AppName { get; set; }
            public string VPN_ServerName { get; set; }
        }

        List<IpInfo> info = new List<IpInfo>();

        public IPMap()
        {
            Directory.CreateDirectory("MoonSpoofer");
            if (File.Exists("MoonSpoofer/ipinfo.json"))
            {
                info = JsonConvert.DeserializeObject<List<IpInfo>>(File.ReadAllText("MoonSpoofer/ipinfo.json"));
            }
        }

        bool AddRecord(String userId, String ip, String hwid, String appName, String serverName)
        {
            info.Add(new IpInfo() { UserID = userId, IP = ip, HWID = hwid, VPN_AppName = appName, VPN_ServerName = serverName });
        }

        IpInfo GetByIP(String ip)
        {
            return info.FirstOrDefault(info => info.IP == ip);
        }
        IpInfo GetByHWID(String hwid)
        {
            return info.FirstOrDefault(info => info.HWID == hwid);
        }
        IpInfo GetByUserID(String userId)
        {
            return info.FirstOrDefault(info => info.UserID == userId);
        }
        IpInfo GetByServerName(String appName, String serverName)
        {
            return info.FirstOrDefault(info => info.VPN_AppName == appName && info.VPN_ServerName == serverName);
        }
    }
}

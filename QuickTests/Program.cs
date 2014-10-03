using CSGODemoParser.Demo;
using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTests
{
    class Program
    {
        public static Dictionary<string, object> configItems = new Dictionary<string, object>();
        static void Main(string[] args)
        {
            FullParser qp = new FullParser(new CSGODemoReader("C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Counter-Strike Global Offensive\\csgo\\draft.dem")); //match730_003030909602041430032_0894226252_185 - Copy (5) - Copy.dem
            DemoFile d = new DemoFile(qp);
            int convarMessage = Netmessages.Descriptor.FindTypeByName<Google.ProtocolBuffers.Descriptors.EnumDescriptor>("NET_Messages").FindValueByName("net_SetConVar").Number;
            qp.RegisterCallback(convarMessage, cb);
            d.Parse();
            foreach (KeyValuePair<String, object> configItem in configItems)
            {
                Console.WriteLine(configItem.Key + " " + configItem.Value);
            }
            Console.ReadLine();
        }
        public static void cb(NetMessage msg)
        {
            CNETMsg_SetConVar cvars = CNETMsg_SetConVar.ParseFrom(msg.Data);
            foreach (CMsg_CVars.Types.CVar cvar in cvars.Convars.CvarsList)
            {
                configItems[cvar.Name] = cvar.Value;
            }
        }
    }
}

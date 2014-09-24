using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo
{
    enum DemoMessage : byte
    {
        Signon = 1,
        Packet,
        SyncTick,
        ConsoleCMD,
        UserCMD,
        Datatables,
        Stop,
        CustomData,
        StringTables,
        LastCMD = StringTables
    }
}

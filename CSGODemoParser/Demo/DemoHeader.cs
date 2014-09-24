using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo
{
    public class DemoHeader
    {
        public string DemoFile { get;  set; }
        public int Protocol { get;  set; }
        public int NetworkProtocol { get;  set; }
        public string ServerName { get;  set; }
        public string ClientName { get;  set; }
        public string MapName { get;  set; }
        public string GameDirectory { get;  set; }
        public float PlaybackTime { get;  set; }
        public long TickCount { get;  set; }
        public long PlaybackFrames { get;  set; }
        public long SignonLength { get;  set; }
    }
}

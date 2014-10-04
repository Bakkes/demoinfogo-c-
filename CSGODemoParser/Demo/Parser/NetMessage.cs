using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    public class NetMessage
    {
        public int Tick { get; set; }
        public int Command { get; set; }
        public int CommandSize { get; set; }
        public byte[] Data { get; set; }
    }
}

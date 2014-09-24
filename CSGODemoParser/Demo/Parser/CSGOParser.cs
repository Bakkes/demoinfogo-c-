using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    abstract class CSGOParser : IDemoParser
    {
        protected IDemoReader demoReader { get; set; }

        public CSGOParser(IDemoReader demoReader)
        {
            this.demoReader = demoReader;
        }

        public DemoHeader ParseHeader()
        {
            return new DemoHeader()
            {
                DemoFile = demoReader.ReadString(8, true),
                Protocol = demoReader.ReadInt32(),
                NetworkProtocol = demoReader.ReadInt32(),
                ServerName = demoReader.ReadString(260, true),
                ClientName = demoReader.ReadString(260, true),
                MapName = demoReader.ReadString(260, true),
                GameDirectory = demoReader.ReadString(260, true),
                PlaybackTime = demoReader.ReadFloat(),
                TickCount = demoReader.ReadInt32(),
                PlaybackFrames = demoReader.ReadInt32(),
                SignonLength = demoReader.ReadInt32()
            };
        }
    }
}

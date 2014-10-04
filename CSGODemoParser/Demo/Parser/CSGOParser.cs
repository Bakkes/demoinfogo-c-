using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    public abstract class CSGOParser : IDemoParser
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

        protected int readInt32(byte[] buf, int bufLength, ref int index)
        {
            int b;
            int count = 0;
            int result = 0;
            do
            {
                if (count == 5)
                {
                    return result;
                }
                else if (index >= bufLength)
                {
                    return result;
                }
                b = buf[index++];
                result |= (b & 0x7F) << (7 * count);
                ++count;
            } while ((b & 0x80) != 0);

            return result;
        }


        protected byte[] readRawData()
        {
            int size = demoReader.ReadInt32();
            return demoReader.ReadBytes(size);
        }

        protected void readUserCmd()
        {
            int outgoing = demoReader.ReadInt32();
            byte[] data = readRawData();
        }

        public abstract void Parse();
    }
}

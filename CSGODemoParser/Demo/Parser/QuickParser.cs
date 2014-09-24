using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    //Class that quickly finds the RankReveal message, skips over the rest
    public class QuickParser : CSGOParser
    {
        public QuickParser(IDemoReader reader) : base(reader)
        {

        }

        public void Parse()
        {
            bool finished = false;
            while (!finished)
            {
                //cmd, tick, playerslot = self.demofile.read_cmd_header()
                byte cmd = demoReader.ReadByte();
                int tick = demoReader.ReadInt32();
                byte playerSlot = demoReader.ReadByte();
                DemoMessage message = (DemoMessage)cmd;
                switch (message)
                {
                    case DemoMessage.SyncTick:
                        continue;
                    case DemoMessage.Stop:
                        finished = true;
                        break;
                    case DemoMessage.ConsoleCMD:
                    case DemoMessage.Datatables:
                    case DemoMessage.StringTables:
                        ignoreRawData();
                        break;
                    case DemoMessage.UserCMD:
                        readUserCmd();
                        break;
                    case DemoMessage.Signon:
                    case DemoMessage.Packet:
                        parsePacket();
                        break;
                }
            }
        }

        private void parsePacket()
        {
            demoReader.ReadBytes(152); //ignore cmdinfo
            int seq_in = demoReader.ReadInt32();
            int seq_out = demoReader.ReadInt32();
            ignoreRawData();
        }

        private void ignoreRawData()
        {
            int size = demoReader.ReadInt32();
            demoReader.ReadBytes(size);
        }

        private void readUserCmd()
        {
            int outgoing = demoReader.ReadInt32();
            ignoreRawData();
        }
    }
}

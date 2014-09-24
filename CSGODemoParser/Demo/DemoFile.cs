using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo
{
    public class DemoFile
    {
        private const string STRING_TRIMMER = ".TrimEnd(new char[]{'\0'})";

        public DemoHeader Header { get; private set; }
        public string FileLocation { get; private set; }
        private IDemoReader demoReader { get; set; }

        public DemoFile(string fileLocation)
        {
            this.FileLocation = fileLocation;
            this.demoReader = new CSGODemoReader(fileLocation);
            parseHeader();
            Parse();
        }

        private void parseHeader()
        {
            Header = new DemoHeader()
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
                //Console.WriteLine("Test");
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

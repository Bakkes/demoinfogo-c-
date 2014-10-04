using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    //Class that quickly finds the RankReveal message, skips over the rest
    //Please note this is optimized for speed, if you have any other optimizations, let me know
    public delegate void RankUpdated(CCSUsrMsg_ServerRankUpdate rankupdate);
    public class QuickParser : CSGOParser
    {
        public CCSUsrMsg_ServerRankUpdate RankUpdate; 
        public event RankUpdated OnRankUpdate;

        public QuickParser(IDemoReader reader) : base(reader)
        {

        }

        int tick = 0;
        bool finished = false;
        public override void Parse()
        {
            while (!finished)
            {
                //cmd, tick, playerslot = self.demofile.read_cmd_header()
                byte cmd = demoReader.ReadByte();
                tick = demoReader.ReadInt32();//119228
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
                        readRawData();
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
            int size = demoReader.ReadInt32();
            byte[] data = demoReader.ReadBytes(size);

            //BinaryReader binaryReader = new BinaryReader(new MemoryStream(data));
            int index = 0;
            while (index < size)
            {
                int cmd = readInt32(data, size, ref index);
                int cmdSize = readInt32(data, size, ref index);
                if (cmd == 23)
                {
                    byte[] cmdData = new byte[cmdSize];
                    Array.Copy(data, index, cmdData, 0, cmdSize);
                    CSVCMsg_UserMessage msg = CSVCMsg_UserMessage.ParseFrom(cmdData);
                    if (msg.MsgType == 52)
                    {
                        RankUpdate = CCSUsrMsg_ServerRankUpdate.ParseFrom(msg.MsgData);
                        if(OnRankUpdate != null)
                            OnRankUpdate(RankUpdate);
                        finished = true;
                        return;
                    }
                }
                
                index += cmdSize;
            }
        }
    }
}

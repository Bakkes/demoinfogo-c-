using CSGODemoParser.Demo.Parser.ParseObjects;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.Parser
{
    public class FullParser : CSGOParser
    {

        public FullParser(IDemoReader reader)
            : base(reader)
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

        private T ParseXYZ<T>() where T : XYZValue, new()
        {
            return new T()
            {
                x = demoReader.ReadInt32(),
                y = demoReader.ReadInt32(),
                z = demoReader.ReadInt32()
            };
        }

        private Split parseSplit()
        {
            return new Split()
                {
                    flags = demoReader.ReadInt32(),
                    viewOrigin = ParseXYZ<Vector>(),
                    viewAngles = ParseXYZ<QAngle>(),
                    localViewAngles = ParseXYZ<QAngle>(),
                    viewOrigin2 = ParseXYZ<Vector>(),
                    viewAngles2 = ParseXYZ<QAngle>(),
                    localViewAngles2 = ParseXYZ<QAngle>(),
                };
        }

        private DemoCmdInfo ReadCMDInfo()
        {
            return new DemoCmdInfo()
            {
                playerOne = parseSplit(),
                playerTwo = parseSplit()
            };
        }

        private void parsePacket()
        {
            DemoCmdInfo i = ReadCMDInfo(); 
            int seq_in = demoReader.ReadInt32();
            int seq_out = demoReader.ReadInt32();
            int size = demoReader.ReadInt32();
            byte[] data = demoReader.ReadBytes(size);

            //BinaryReader binaryReader = new BinaryReader(new MemoryStream(data));
            int index = 0;
            while (index < size)
            {
                int cmd = customInt32(data, size, ref index);
                int cmdSize = customInt32(data, size, ref index);
                if (cmd == 23)
                {
                    byte[] cmdData = new byte[cmdSize];
                    Array.Copy(data, index, cmdData, 0, cmdSize);
                    CSVCMsg_UserMessage msg = CSVCMsg_UserMessage.ParseFrom(cmdData);
                    if (msg.MsgType == 52)
                    {
                        finished = true;
                        return;
                    }
                }
                
                index += cmdSize;
            }
        }

        private int customInt32(byte[] buf, int bufLength, ref int index)
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

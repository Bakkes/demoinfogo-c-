﻿using CSGODemoParser.Demo.Parser.ParseObjects;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pb = global::Google.ProtocolBuffers;

namespace CSGODemoParser.Demo.Parser
{
    public delegate void NetMessageCallback(NetMessage message);

    public class FullParser : CSGOParser
    {
        private Dictionary<int, List<NetMessageCallback>> callbacks = new Dictionary<int, List<NetMessageCallback>>();
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
                byte playerSlot = demoReader.ReadByte(); //always 0?
                //Console.WriteLine(tick);
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

        private T parseXYZ<T>() where T : XYZValue, new()
        {
            return new T()
            {
                x = demoReader.ReadInt32(),
                y = demoReader.ReadInt32(),
                z = demoReader.ReadInt32()
            };
        }

        public void RegisterCallback(int command, NetMessageCallback callback) 
        {
            if(!callbacks.ContainsKey(command)) {
                callbacks.Add(command, new List<NetMessageCallback>());
            }
            callbacks[command].Add(callback);
        }

        private Split parseSplit()
        {
            return new Split()
                {
                    flags = demoReader.ReadInt32(),
                    viewOrigin = parseXYZ<Vector>(),
                    viewAngles = parseXYZ<QAngle>(),
                    localViewAngles = parseXYZ<QAngle>(),
                    viewOrigin2 = parseXYZ<Vector>(),
                    viewAngles2 = parseXYZ<QAngle>(),
                    localViewAngles2 = parseXYZ<QAngle>(),
                };
        }

        private DemoCmdInfo readCMDInfo()
        {
            return new DemoCmdInfo()
            {
                playerOne = parseSplit(),
                playerTwo = parseSplit()
            };
        }

        private void parsePacket()
        {
            DemoCmdInfo i = readCMDInfo();
            int seq_in = demoReader.ReadInt32();
            int seq_out = demoReader.ReadInt32();
            int size = demoReader.ReadInt32();
            byte[] data = demoReader.ReadBytes(size);

            int index = 0;
            while (index < size)
            {
                int cmd = readInt32(data, size, ref index);
                int cmdSize = readInt32(data, size, ref index);
                if (callbacks.ContainsKey(cmd))
                {
                    NetMessage msg = new NetMessage()
                    {
                        Command = cmd,
                        CommandSize = cmdSize,
                        Data = new byte[cmdSize]
                    };
                    Array.Copy(data, index, msg.Data, 0, cmdSize);

                    foreach (NetMessageCallback callback in this.callbacks[cmd])
                    {
                        callback(msg);
                    }
                }
                
                index += cmdSize;
            }
        }

        private int readInt32(byte[] buf, int bufLength, ref int index)
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

        private byte[] ignoreRawData()
        {
            int size = demoReader.ReadInt32();
            return demoReader.ReadBytes(size);
        }

        private void readUserCmd()
        {
            int outgoing = demoReader.ReadInt32();
            byte[] data = ignoreRawData();
            int b = 0;
        }
    }
}

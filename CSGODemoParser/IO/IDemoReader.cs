using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.IO
{
    interface IDemoReader
    {
        int ReadInt16();
        int ReadInt32();
        long ReadLong();
        float ReadFloat();
        char ReadUChar();
        string ReadString(int length, bool trimEnd=false);
        byte ReadByte();
        byte[] ReadBytes(int length);
        void Seek(long offset, SeekOrigin origin);
        void Skip(int count);
    }
}

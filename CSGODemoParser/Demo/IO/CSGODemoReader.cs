using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.IO
{
    public class CSGODemoReader : IDemoReader
    {
        private static readonly char[] TRIM_CHARS = {'\0'};

        public long Position
        {
            get
            {
                return binaryReader.BaseStream.Position;
            }
        }

        private string fileLocation { get; set; }
        private FileStream fileStream { get; set; }
        private BinaryReader binaryReader { get; set; }

        public CSGODemoReader(string fileLocation)
        {
            this.fileLocation = fileLocation;
            this.fileStream = File.OpenRead(fileLocation);
            this.binaryReader = new BinaryReader(this.fileStream);
        }

        public int ReadInt16()
        {
            return binaryReader.ReadInt16();
        }

        public int ReadInt32()
        {
            return binaryReader.ReadInt32();
        }

        public long ReadLong()
        {
            return binaryReader.ReadInt64();
        }

        public float ReadFloat()
        {
            return binaryReader.ReadSingle();
        }

        public char ReadUChar()
        {
            return 'a';
        }

        public string ReadString(int length, bool trimEnd=false)
        {
            string result = System.Text.Encoding.Default.GetString(binaryReader.ReadBytes(length));
            if (trimEnd)
                result = result.TrimEnd(TRIM_CHARS);
            return result;
        }

        public byte ReadByte()
        {
            return binaryReader.ReadByte();
        }

        public byte[] ReadBytes(int length)
        {
            return binaryReader.ReadBytes(length);
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            binaryReader.BaseStream.Seek(offset, origin);
        }

        public void Skip(int count)
        {
            this.Seek(count, SeekOrigin.Current);
        }
    }
}

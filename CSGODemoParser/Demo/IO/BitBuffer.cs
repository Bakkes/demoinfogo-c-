using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGODemoParser.Demo.IO
{
    class BitBuffer
    {
        private byte[] buffer;

        uint inBufWord;
        int bitsAvailable;
        uint dataIn;
        uint bufferEnd;
        uint data;

        bool overflow;
        int dataBits;
        int dataBytes;
        

        public BitBuffer(byte[] input, int bytes, int bits = -1)
        {
            this.buffer = input;
            overflow = false;
            dataBits = -1;
            dataBytes = 0;
            this.dataBytes = bytes;
            this.dataBits = bits;
        }

        public void setOverFlowFlag()
        {
            this.overflow = true;
        }

        public bool isOverflowed()
        {
            return overflow;
        }

        public int Tell()
        {
            return GetNumBitsRead();
        }

        public int GetTotalBytesAvailable()
        {
            return dataBytes;
        }

        public int GetNumBitsLeft()
        {
            return dataBits - Tell();
        }

        public int GetNumBytesLeft()
        {
            return GetNumBitsLeft() >> 3;
        }

        public bool SeekRelative(int offset)
        {
            return Seek(GetNumBitsRead() + offset);
        }


        public int GetNumBitsRead()
        {
            int curDfs = (((int)dataIn - (int)data) / 4) - 1;
            curDfs *= 32;
            curDfs += (32 - bitsAvailable);
            int adjust = 8 * (dataBytes & 3);
            return Math.Min(curDfs + adjust, dataBits);
        }

        public int GetNumBytesRead()
        {
            return ((GetNumBitsRead() + 7) >> 3);
        }

        public void GrabNextDWord(bool overflowImmediately)
        {
            if (dataIn == bufferEnd)
            {
                bitsAvailable = 1;
                inBufWord = 0;
                dataIn++;
                if (overflowImmediately)
                {
                    setOverFlowFlag();
                }
            }
            else if (dataIn > bufferEnd)
            {
                setOverFlowFlag();
                inBufWord = 0;
            }
            else
            {
                inBufWord = dataIn++;
            }
        }

        public void FetchNext()
        {
            bitsAvailable = 32;
            GrabNextDWord(false);
        }

        public int ReadOneBit()
        {
            int ret = (int)inBufWord & 1;
            if (--bitsAvailable == 0)
            {
                FetchNext();
            }
            else
            {
                inBufWord >>= 1;
            }
            return ret;
        }

        public uint ReadUBitLong(int numbits)
        {
            if (bitsAvailable >= numbits)
            {
                uint ret = inBufWord & maskTable[numbits];
                bitsAvailable -= numbits;
                if (bitsAvailable > 0)
                {
                    inBufWord >>= numbits;
                }
                else
                {
                    FetchNext();
                }
                return ret;
            }
            else
            {
                uint ret = inBufWord;
                numbits -= bitsAvailable;
                GrabNextDWord(true);
                if (overflow)
                    return 0;
                ret |= ((inBufWord & maskTable[numbits]) << bitsAvailable);
                bitsAvailable = 32 - numbits;
                inBufWord >>= numbits;
                return ret;
            }
        }

        public int ReadSBitLong(int numbits)
        {
            int ret = (int)ReadUBitLong(numbits);
            return (ret << (32 - numbits)) >> (32 - numbits);
        }

        public uint ReadUBitVar()
        {
            uint ret = ReadUBitLong(6);
            switch (ret & (16 | 32))
            {
                case 16:
                    ret = (ret & 15) | (ReadUBitLong(4) << 4);
                    break;
                case 32:
                    ret = (ret & 15) | (ReadUBitLong(8) << 4);
                    break;
                case 48:
                    ret = (ret & 15) | (ReadUBitLong(32 - 4) << 4);
                    break;
            }
            return ret;
        }

        public int ReadChar()
        {
            return (int)ReadUBitLong(sizeof(char) << 3);
        }

        public int ReadByte()
        {
            return (int)ReadUBitLong(sizeof(byte) << 3);
        }

        public int ReadShot()
        {
            return ReadSBitLong(sizeof(short) << 3);
        }

        public int ReadWord()
        {
            return (int)ReadUBitLong(sizeof(UInt16) << 3);
        }

        public bool Seek(int position)
        {
            bool succes = true;

            if (position < 0 || position > dataBits)
            {
                setOverFlowFlag();
                succes = false;
                position -= dataBits;
            }
            int head = dataBytes & 3;
            int byteDfs = position / 8;
            if ((dataBytes < 4) || (head != 0 && (byteDfs < head)))
            {

            }
            return false;
        }


        public Int32 ReadVarInt32()
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
                b = (int)ReadUBitLong(8);
                result |= (b & 0x7F) << (7 * count);
                ++count;
            } while ((b & 0x80) != 0);

            return result;
        }

        private static readonly uint[] maskTable = {
	        0,
	        ( 1 << 1 ) - 1,
	        ( 1 << 2 ) - 1,
	        ( 1 << 3 ) - 1,
	        ( 1 << 4 ) - 1,
	        ( 1 << 5 ) - 1,
	        ( 1 << 6 ) - 1,
	        ( 1 << 7 ) - 1,
	        ( 1 << 8 ) - 1,
	        ( 1 << 9 ) - 1,
	        ( 1 << 10 ) - 1,
	        ( 1 << 11 ) - 1,
	        ( 1 << 12 ) - 1,
	        ( 1 << 13 ) - 1,
	        ( 1 << 14 ) - 1,
	        ( 1 << 15 ) - 1,
	        ( 1 << 16 ) - 1,
	        ( 1 << 17 ) - 1,
	        ( 1 << 18 ) - 1,
	        ( 1 << 19 ) - 1,
   	        ( 1 << 20 ) - 1,
	        ( 1 << 21 ) - 1,
	        ( 1 << 22 ) - 1,
	        ( 1 << 23 ) - 1,
	        ( 1 << 24 ) - 1,
	        ( 1 << 25 ) - 1,
	        ( 1 << 26 ) - 1,
   	        ( 1 << 27 ) - 1,
	        ( 1 << 28 ) - 1,
	        ( 1 << 29 ) - 1,
	        ( 1 << 30 ) - 1,
	        0x7fffffff,
	        0xffffffff,
        };
    }
}

using System;

namespace NESEmulator
{
    public class BytesUtils
    {
        public static ushort CombineBytes(byte hi, byte lo)
        {
            return (ushort)((hi << 8) | lo);
        }

        public static byte GetHiByte(ushort address)
        {
            return (byte)(address >> 8);
        }

        public static byte GetLoByte(ushort address)
        {
            return (byte)address;
        }

        public static bool GetMSB(byte data)
        {
            return data > 0b01111111;
        }
        public static bool GetLSB(byte data)
        {
            return data % 2 == 1;
        }


        //returns a ushort which is the union of 0x00 and the sum of the addends WITH NO CARRY
        public static ushort ZeroPageSum(byte addend1, byte addend2)
        {
            return CombineBytes(0x00, (byte)(addend1 + addend2));
        }
    }
}

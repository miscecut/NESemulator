using System;

namespace NESEmulator.Cartridge
{
    public class CartridgeHeader
    {
        public byte[] Name { get; private set; }
        public byte SizeOfProgramROM { get; private set; } //In 16 KB units
        public byte SizeOfCharacterROM { get; private set; } //In 8 KB units
        public byte MapperFlags1 { get; private set; }
        public byte MapperFlags2 { get; private set; }
        public byte ProgramRAMFlags { get; private set; }
        public byte TVSystemFlags1 { get; private set; }
        public byte TVSystemFlags2 { get; private set; }
        public byte[] Unused { get; private set; }

        //The first 16 Bytes of the .nes file shuld be passed here
        public CartridgeHeader(byte[] bytes) {
            Name = BytesUtils.GetByteRange(bytes, 0, 4); //The first 4 Bytes are the Name
            SizeOfProgramROM = bytes[4];
            SizeOfCharacterROM = bytes[5];
            MapperFlags1 = bytes[6];
            MapperFlags2 = bytes[7];
            ProgramRAMFlags = bytes[8];
            TVSystemFlags1 = bytes[9];
            TVSystemFlags2 = bytes[10];
            Unused = BytesUtils.GetByteRange(bytes, 11, 16); //The last 5 Bytes are unused
        }

        public bool IsTrainerPresent()
        {
            return (MapperFlags1 & 0b00000100) > 0;
        }

        public byte GetMapperId()
        {
            byte hiNibbleMapperId = (byte)(MapperFlags2 & 0xF0);
            byte loNibbleMapperId = (byte)((MapperFlags1 & 0xF0) >> 4);
            return (byte)(hiNibbleMapperId | loNibbleMapperId);
        }
    }
}

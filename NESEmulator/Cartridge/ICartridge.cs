namespace NESEmulator.Cartridge
{
   public  interface ICartridge
    {
        //Communication with mCPU
        public bool CPURead(ushort address, ref byte data); //returns true if the data requested was actually from the catridge (in this case, the data is returned by reference)
        public bool CPUWrite(ushort address, byte data);
        //Communication with PPU
        public bool PPURead(ushort address, ref byte data);
        public bool PPUWrite(ushort address, byte data);
    }
}

namespace NESEmulator.Cartridge
{
   public  interface ICartridge
    {
        //Communication with mCPU
        public byte CPURead(ushort address);
        public void CPUWrite(ushort address, byte data);
        //Communication with PPU
        public byte PPURead(ushort address);
        public void PPUWrite(ushort address, byte data);
    }
}

namespace NESEmulator.PPU
{
    public interface IPPU
    {
        //Communication with main bus, this exposes the 8 registers in the PPU
        public byte CPURead(ushort address); 
        public void CPUWrite(ushort address, byte data);
        //Communication with PPU's bus
        public byte PPURead(ushort address);
        public void PPUWrite(ushort address, byte data);
        //clock signal
        public void Clock();
    }
}

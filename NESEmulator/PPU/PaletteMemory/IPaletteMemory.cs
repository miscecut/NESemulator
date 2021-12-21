namespace NESEmulator.PPU
{
    public interface IPaletteMemory
    {
        public byte Read(ushort address);
        public void Write(ushort address, byte data);
    }
}

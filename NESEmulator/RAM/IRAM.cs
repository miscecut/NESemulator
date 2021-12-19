namespace NESEmulator.RAM
{
    public interface IRAM
    {
        public byte Read(ushort address);
        public void Write(ushort address, byte data);
    }
}

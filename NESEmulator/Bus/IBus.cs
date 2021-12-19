using NESEmulator.Cartridge;

namespace NESEmulator.Bus
{
    public interface IBus
    {
        public byte CPURead(ushort address); //ONLY the CPU reads and writes from the bus
        public void CPUWrite(ushort address, byte data);
        public void InsertCartridge(ICartridge cartridge);
    }
}

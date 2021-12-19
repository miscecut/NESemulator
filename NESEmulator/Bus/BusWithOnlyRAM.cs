using NESEmulator.Cartridge;
using NESEmulator.RAM;
namespace NESEmulator.Bus
{
    //This class is used mainly for testing, it's a bus with only a ram;
    public class BusWithOnlyRAM : IBus
    {
        private IRAM _ram;

        public BusWithOnlyRAM()
        {
            _ram = new NormalRAM();
        }

        public byte CPURead(ushort address)
        {
            return _ram.Read(address);
        }

        public void CPUWrite(ushort address, byte data)
        {
            _ram.Write(address, data);
        }

        public void InsertCartridge(ICartridge cartridge)
        {
            return;
        }

        public void Reset()
        {
            return;
        }
    }
}

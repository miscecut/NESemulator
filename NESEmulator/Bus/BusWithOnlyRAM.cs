using NESEmulator.Cartridge;
using NESEmulator.CPU;
using NESEmulator.RAM;
namespace NESEmulator.Bus
{
    //This class is used mainly for testing, it's a bus with only a ram;
    public class BusWithOnlyRAM : IBus
    {
        private IRAM _ram;
        private ICPU _cpu;

        public BusWithOnlyRAM(ICPU cpu)
        {
            _ram = new NormalRAM();
            _cpu = cpu;
        }

        public BusWithOnlyRAM()
        {
            _ram = new NormalRAM();
            _cpu = new NMOS6502(this, new CPURegisters());
        }

        public void Clock()
        {
            _cpu.Clock();
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
            _cpu.Reset();
        }
    }
}

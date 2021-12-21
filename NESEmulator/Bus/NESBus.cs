using NESEmulator.Cartridge;
using NESEmulator.CPU;
using NESEmulator.PPU;
using NESEmulator.RAM;

namespace NESEmulator.Bus
{
    public class NESBus : IBus
    {
        private ulong _systemClocks;
        private readonly ICPU _cpu;
        private readonly IRAM _ram;
        private readonly IPPU _ppu;
        private ICartridge _cartridge;

        public NESBus()
        {
            _systemClocks = 0;
            _cpu = new NMOS6502(this, new CPURegisters());
            _ram = new MirroringRAM(); //0x0000 - 0x1FFF
            _ppu = new PPU2C02(); //0x2000 - 0x3FFF
        }

        public void Clock()
        {
            _ppu.Clock();

            //CPU's clock is 3 times slower
            if(_systemClocks % 3 == 0)
                _cpu.Clock();

            _systemClocks++;
        }

        public byte CPURead(ushort address)
        {
            byte retrievedData = 0x00;
            //The cartridge has first dibs on what the CPU is trying to read on the bus
            if (_cartridge.CPURead(address, ref retrievedData))
                return retrievedData;

            if (address < 0x2000) //RAM address
                return _ram.Read(address);
            if (address < 0x4000) //PPU address
                return _ppu.CPURead(address);
            return 0x00;
        }

        public void CPUWrite(ushort address, byte data)
        {
            //The cartridge has first dibs on what the CPU is trying to write on the bus
            var cartridgeWritten = _cartridge.CPUWrite(address, data);

            if (!cartridgeWritten)
            {
                if (address < 0x2000) //RAM address
                    _ram.Write(address, data);
                else if (address < 0x4000) //PPU address
                    _ppu.CPUWrite(address, data);
            }
        }

        public void InsertCartridge(ICartridge cartridge)
        {
            _cartridge = cartridge;
            _ppu.InsertCartridge(cartridge);
        }

        public void Reset()
        {
            _systemClocks = 0;
            _cpu.Reset();
        }
    }
}

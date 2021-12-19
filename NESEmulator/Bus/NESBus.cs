using NESEmulator.Cartridge;
using NESEmulator.CPU;
using NESEmulator.PPU;
using NESEmulator.RAM;

namespace NESEmulator.Bus
{
    public class NESBus : IBus
    {
        private readonly ICPU _cpu;
        private readonly IRAM _ram;
        private readonly IPPU _ppu;
        private ICartridge _cartridge;

        public NESBus()
        {
            _cpu = new NMOS6502(this);
            _ram = new MirroringRAM(); //0x0000 - 0x1FFF
        }

        public byte CPURead(ushort address)
        {
            if (address < 0x2000) //RAM address
                return _ram.Read(address);
            if (address < 0x4000) //PPU address
                return _ppu.CPURead(address);
            return 0x00;
        }

        public void CPUWrite(ushort address, byte data)
        {
            if (address < 0x2000) //RAM address
                _ram.Write(address, data);
            else if (address < 0x4000) //PPU address
                _ppu.CPUWrite(address, data);

        }

        public void InsertCartridge(ICartridge cartridge)
        {
            _cartridge = cartridge;
        }
    }
}

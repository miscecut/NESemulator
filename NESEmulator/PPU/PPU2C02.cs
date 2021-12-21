using NESEmulator.Cartridge;
using NESEmulator.PPU.PaletteMemory;

namespace NESEmulator.PPU
{
    public class PPU2C02 : IPPU
    {
        private readonly IPaletteMemory _paletteMemory;
        private readonly ICartridge _cartridge;
        //utils for drawing the screen
        private int _scanLine;
        private int _cycles;
        private bool _frameComplete;

        public PPU2C02()
        {
            _paletteMemory = new NESPaletteMemory();
        }

        public void Clock()
        {
            _cycles++;
            if(_cycles >= 341)
            {
                _cycles = 0;
                _scanLine++;
                if(_scanLine >= 261)
                {
                    _scanLine = -1;
                    _frameComplete = true;
                }
            }
        }

        public byte CPURead(ushort address)
        {
            return 0;
        }

        public void CPUWrite(ushort address, byte data)
        {
            throw new System.NotImplementedException();
        }

        public void InsertCartridge(ICartridge cartridge)
        {
            throw new System.NotImplementedException();
        }

        public byte PPURead(ushort address)
        {
            byte retrievedData = 0x00;
            //The cartridge has first dibs on what the PPU is trying to read on the cartridge
            if (_cartridge.PPURead(address, ref retrievedData))
                return retrievedData;

            if (0x3F00 <= address && address < 0x4000)
                return _paletteMemory.Read(address);

            return 0x00;
        }

        public void PPUWrite(ushort address, byte data)
        {
            //The cartridge has first dibs on what the PPU is trying to write on the cartridge
            var cartridgeWritten = _cartridge.PPUWrite(address, data);

            if(!cartridgeWritten)
            {
                if (0x3F00 <= address && address < 0x4000)
                    _paletteMemory.Write(address, data);
            }
        }
    }
}

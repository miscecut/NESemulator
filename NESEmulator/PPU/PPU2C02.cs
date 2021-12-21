using NESEmulator.Cartridge;

namespace NESEmulator.PPU
{
    public class PPU2C02 : IPPU
    {
        private int _scanLine;
        private int _cycles;
        private bool _frameComplete;

        public PPU2C02()
        {
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
            throw new System.NotImplementedException();
        }

        public void PPUWrite(ushort address, byte data)
        {
            throw new System.NotImplementedException();
        }
    }
}

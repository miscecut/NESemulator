namespace NESEmulator.PPU
{
    public class PPU2C02 : IPPU
    {
        private readonly byte[] _registers;

        public PPU2C02()
        {
            _registers = new byte[8];
        }

        public void Clock()
        {
            throw new System.NotImplementedException();
        }

        public byte CPURead(ushort address)
        {
            return _registers[address & 0x0003];
        }

        public void CPUWrite(ushort address, byte data)
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

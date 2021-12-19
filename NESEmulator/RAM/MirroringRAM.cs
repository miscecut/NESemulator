namespace NESEmulator.RAM
{
    //The actual RAM of the NES, ADDRESS FROM 0x0000 TO 0x1FFF
    public class MirroringRAM : IRAM
    {
        private readonly byte[] _memory;
        private readonly int _mirrorSize;

        public MirroringRAM()
        {
            _memory = new byte[2048]; //2KB RAM
            _mirrorSize = 2048;
        }

        public byte Read(ushort address)
        {
            if (address >= _memory.Length * 4) //out of boundaries
                return 0x00;
            return _memory[address & 0x07FF];
        }

        public void Write(ushort address, byte data)
        {
            if (address < _memory.Length * 4)
                _memory[address & 0x07FF] = data;
        }
    }
}

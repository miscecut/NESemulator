namespace NESEmulator.RAM
{
    //this is used mainly for testing
    public class NormalRAM : IRAM
    {
        private byte[] _memory;

        public NormalRAM()
        {
            _memory = new byte[64 * 1024]; //64KB, 16-address
        }

        public byte Read(ushort address)
        {
            return _memory[address];
        }

        public void Write(ushort address, byte data)
        {
            _memory[address] = data;
        }
    }
}

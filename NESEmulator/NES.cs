using NESEmulator.Bus;
using NESEmulator.Cartridge;

namespace NESEmulator
{
    public class NES
    {
        private readonly IBus _bus;

        public NES(IBus bus)
        {
            _bus = bus;
        }

        public void InsertCartridge(ICartridge cartridge)
        {
            _bus.InsertCartridge(cartridge);
        }

        public void Reset()
        {
            _bus.Reset();
        }
    }
}

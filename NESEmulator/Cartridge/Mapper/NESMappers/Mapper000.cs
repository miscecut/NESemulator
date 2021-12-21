namespace NESEmulator.Cartridge.Mapper.NESMappers
{
    class Mapper000 : IMapper
    {
        private readonly byte _numberOfProgramBanks;
        private readonly byte _numberOfCharactersBanks;
        private readonly ushort _mask;

        public Mapper000(byte numberOfProgramBanks, byte numberOfCharacterBanks)
        {
            _numberOfProgramBanks = numberOfProgramBanks;
            _numberOfCharactersBanks = numberOfCharacterBanks;
            _mask = (ushort)(_numberOfProgramBanks > 1 ? 0x7FFF : 0x3FFF);
        }

        public MapperResponse CPUMapRead(ushort address)
        {
            if (0x8000 <= address && address <= 0xFFFF)
                return new MapperResponse
                {
                    MappedAddress = (ushort)(address & _mask),
                    Consistent = true
                };
            return new MapperResponse
            {
                MappedAddress = address,
                Consistent = false
            };
        }

        public MapperResponse CPUMapWrite(ushort address, byte data)
        {
            if (0x8000 <= address && address <= 0xFFFF) //cartridge memory range in the cpu's bus
                return new MapperResponse
                {
                    MappedAddress = (ushort)(address & _mask),
                    Consistent = true
                };
            return new MapperResponse
            {
                MappedAddress = address,
                Consistent = false
            };
        }

        public MapperResponse PPUMapRead(ushort address)
        {
            if (0x0000 <= address && address <= 0x1FFF) //sprite memory range
                return new MapperResponse
                {
                    MappedAddress = address, //no mask needed, in the 000 mapper there is always only abank of character ROM (always 8 KB)
                    Consistent = true
                };
            return new MapperResponse
            {
                MappedAddress = address,
                Consistent = false
            };
        }

        public MapperResponse PPUMapWrite(ushort address, byte data)
        {
            if (0x0000 <= address && address <= 0x1FFF && _numberOfCharactersBanks == 0) //if no character memory is present, treat this range as RAM...
                return new MapperResponse
                {
                    MappedAddress = address,
                    Consistent = true
                };
            return new MapperResponse //...but if it's present, then it's a ROM, it can't be overwritten by the PPU
            {
                MappedAddress = address,
                Consistent = false
            };
        }
    }
}

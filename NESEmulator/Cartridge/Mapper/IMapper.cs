namespace NESEmulator.Cartridge.Mapper
{
    public interface IMapper
    {
        public MapperResponse CPUMapRead(ushort address);
        public MapperResponse CPUMapWrite(ushort address, byte data);
        public MapperResponse PPUMapRead(ushort address);
        public MapperResponse PPUMapWrite(ushort address, byte data);
    }
}

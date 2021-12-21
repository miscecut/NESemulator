namespace NESEmulator.Cartridge.Mapper
{
    //This is the response from the IMapper class, it contains the mapped address and a boolean which tells the caller if the information was actually for it
    public class MapperResponse
    {
        public ushort MappedAddress { get; set; }
        public bool Consistent { get; set; }
    }
}

namespace NESEmulator.CPU.InstructionSet
{
    //A simple object which is returned by the addring modes methods that contains the fetched value and how many more cycles it requires
    public class AddressingModeResult
    {
        public ushort Address { get; set; }
        public int AdditionalCycles { get; set; }
    }
}

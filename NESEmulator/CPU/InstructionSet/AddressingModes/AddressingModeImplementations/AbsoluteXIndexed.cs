using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class AbsoluteXIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var hiAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = (ushort)(BytesUtils.CombineBytes(hiAddress, loAddress) + registers.X);
            
            var additionalCycles = 0;
            if (hiAddress != BytesUtils.GetHiByte(address)) //if a page boundary is crossed, the operation requires an additional clock cycle
                additionalCycles++;

            return new AddressingModeResult
            {
                AdditionalCycles = additionalCycles,
                Address = address
            };
        }
    }
}

using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class Relative : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loRelativeAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var relativeAddress = BytesUtils.GetMSB(loRelativeAddress) ? BytesUtils.CombineBytes(0xFF, loRelativeAddress) : BytesUtils.CombineBytes(0x00, loRelativeAddress);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = relativeAddress
            };
        }
    }
}

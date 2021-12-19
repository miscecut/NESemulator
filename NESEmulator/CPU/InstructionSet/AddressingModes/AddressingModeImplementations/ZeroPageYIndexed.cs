using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class ZeroPageYIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = BytesUtils.ZeroPageSum(loAddress, registers.Y);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = address
            };
        }
    }
}

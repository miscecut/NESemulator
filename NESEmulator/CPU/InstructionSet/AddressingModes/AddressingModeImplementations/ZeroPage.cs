using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class ZeroPage : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var operand = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = BytesUtils.CombineBytes(0x00, operand);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = address
            };
        }
    }
}

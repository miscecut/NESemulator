using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class Absolute : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var hiAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = BytesUtils.CombineBytes(hiAddress, loAddress);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = address
            };
        }
    }
}

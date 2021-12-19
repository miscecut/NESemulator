using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class ZeroPageYIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = BytesUtils.ZeroPageSum(loAddress, registers.GetRegister(Register.Y));

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = address
            };
        }
    }
}

using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class ZeroPageXIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = BytesUtils.ZeroPageSum(loAddress, registers.GetRegister(Register.X));

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = address
            };
        }
    }
}

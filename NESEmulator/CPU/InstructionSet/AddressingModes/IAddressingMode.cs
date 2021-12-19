using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes
{
    public interface IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers);
    }
}

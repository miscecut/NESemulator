using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes
{
    public interface IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers);
    }
}

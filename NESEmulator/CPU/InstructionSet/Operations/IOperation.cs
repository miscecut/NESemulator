using NESEmulator.Bus;
using NESEmulator.CPU;

namespace NESEmulator.CPU.InstructionSet.Operations
{
    public interface IOperation
    {
        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address);
        public int OperationImmediate(IBus bus, CPURegisters registers);
    }
}

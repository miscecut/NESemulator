using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations
{
    public interface IOperation
    {
        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address);
        public int OperationImmediate(IBus bus, ICPURegisters registers);
    }
}

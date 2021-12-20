using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations
{
    public abstract class ImpliedOperation : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            return OperationImplied(bus, registers);
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return OperationImplied(bus, registers);
        }

        protected abstract int OperationImplied(IBus bus, ICPURegisters registers);
    }
}

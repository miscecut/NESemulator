using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.NopOperations
{
    public class NOP : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

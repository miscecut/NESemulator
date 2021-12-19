using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.NopOperations
{
    public class NOP : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

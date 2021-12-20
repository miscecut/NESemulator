using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.NopOperations
{
    public class Nop : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            return 0;
        }
    }
}

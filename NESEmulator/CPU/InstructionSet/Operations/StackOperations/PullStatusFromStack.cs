using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullStatusFromStack : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            var status = bus.CPURead((ushort)(registers.GetRegister(Register.StackPointer) + 0x0100));
            registers.SetStatus(status);
            registers.SetFlag(StatusRegisterFlags.Unused, true); //the U flag must always be set

            return 0;
        }
    }
}

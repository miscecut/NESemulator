using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullStatusFromStack : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            var status = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            registers.SetStatus(status);
            registers.SetFlag(StatusRegisterFlags.Unused, true); //the U flag must always be set

            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullStatusFromStack : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            var status = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            registers.Status = status;
            registers.SetFlag(StatusRegisterFlags.Unused, true); //the U flag must always be set

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

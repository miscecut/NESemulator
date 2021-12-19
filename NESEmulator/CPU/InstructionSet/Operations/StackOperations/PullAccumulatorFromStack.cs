using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullAccumulatorFromStack : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            registers.A = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.A == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.A));

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

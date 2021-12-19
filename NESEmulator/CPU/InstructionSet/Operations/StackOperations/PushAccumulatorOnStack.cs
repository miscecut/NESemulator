using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PushAccumulatorOnStack : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            //It saves the accumulator un the stack, which is decreased
            bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), registers.A);

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

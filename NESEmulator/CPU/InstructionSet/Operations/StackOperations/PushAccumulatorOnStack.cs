using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PushAccumulatorOnStack : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            //It saves the accumulator un the stack, which is decreased
            bus.CPUWrite((ushort)(0x0100 + registers.GetStackPointer()), registers.GetRegister(Register.A));
            registers.DecrementStackPointer();

            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

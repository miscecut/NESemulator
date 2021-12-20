using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PushAccumulatorOnStack : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            //It saves the accumulator un the stack, which is decreased
            bus.CPUWrite((ushort)(0x0100 + registers.GetRegister(Register.StackPointer)), registers.GetRegister(Register.Accumulator));
            registers.DecrementStackPointer();

            return 0;
        }
    }
}

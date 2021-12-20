using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullAccumulatorFromStack : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            registers.SetRegister(Register.Accumulator, bus.CPURead((ushort)(registers.GetRegister(Register.StackPointer) + 0x0100)));

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.Accumulator) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.Accumulator)));

            return 0;
        }
    }
}

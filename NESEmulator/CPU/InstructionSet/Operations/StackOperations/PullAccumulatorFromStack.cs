using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.StackOperations
{
    public class PullAccumulatorFromStack : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            registers.SetRegister(Register.A, bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100)));

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.A) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.A)));

            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

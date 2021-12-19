using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class Xor : RegistersOperation
    {
        protected override int Operation(ICPURegisters registers, byte operand)
        {
            var newAccumulatorValue = (byte)(registers.GetRegister(Register.A) ^ operand);
            registers.SetRegister(Register.A, newAccumulatorValue);

            //this operation checks the Z & N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.A) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.A)));

            return 0;
        }
    }
}
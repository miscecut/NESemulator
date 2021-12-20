using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class And : RegistersOperation
    {
        protected override int Operation(ICPURegisters registers, byte operand)
        {
            var newAccumulatorValue = (byte)(registers.GetRegister(Register.Accumulator) & operand);
            registers.SetRegister(Register.Accumulator, newAccumulatorValue);

            //this operation checks the Z & N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.Accumulator) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.Accumulator)));

            return 0;
        }
    }
}

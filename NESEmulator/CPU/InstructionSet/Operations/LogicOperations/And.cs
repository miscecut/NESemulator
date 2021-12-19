namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class And : RegistersOperation
    {
        protected override int Operation(CPURegisters registers, byte operand)
        {
            registers.A = (byte)(registers.A & operand);

            //this operation checks the Z & N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.A == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.A));

            return 0;
        }
    }
}

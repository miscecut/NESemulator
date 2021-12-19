namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    //Compare operation
    public class CompareY : RegistersOperation
    {
        protected override int Operation(CPURegisters registers, byte operand)
        {
            byte subtraction = (byte)((ushort)registers.Y - (ushort)operand);

            //this operation sets the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, subtraction == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(subtraction));
            registers.SetFlag(StatusRegisterFlags.Carry, operand <= registers.Y);
            return 0;
        }
    }
}
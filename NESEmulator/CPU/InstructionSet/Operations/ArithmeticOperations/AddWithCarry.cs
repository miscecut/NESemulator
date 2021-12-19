namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class AddWithCarry : RegistersOperation
    {
        protected override int Operation(CPURegisters registers, byte operand)
        {
            var carryAddend = registers.GetFlag(StatusRegisterFlags.Carry) ? 1 : 0;

            ushort sum = (ushort)(registers.A + operand + carryAddend);
            var oldAccumulatorValue = registers.A;
            registers.A = BytesUtils.GetLoByte(sum);

            //Setup logic operators for Overflow Flag
            bool a = BytesUtils.GetMSB(oldAccumulatorValue); //sign of the accumulator before sum (first addend)
            bool m = BytesUtils.GetMSB(operand); //sign of the addend (second addend)
            bool r = BytesUtils.GetMSB(registers.A); //sign of the result
            bool v = (a ^ r) & !(a ^ m);

            //This operations sets or unsets the Z, N, C, V flags
            registers.SetFlag(StatusRegisterFlags.Carry, sum > 0x00FF);
            registers.SetFlag(StatusRegisterFlags.Zero, registers.A == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, r);
            registers.SetFlag(StatusRegisterFlags.Overflow, v);
            //TODO: SET FLAG OVERFLOW

            return 0;
        }
    }
}

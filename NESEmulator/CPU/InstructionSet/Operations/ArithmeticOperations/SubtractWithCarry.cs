using NESEmulator.CPU.Registers;
using System;

namespace NESEmulator.CPU.InstructionSet.Operations.ArithmeticOperations
{
    public class SubtractWithCarry : RegistersOperation
    {
        //A = A - M - (1 - C) =  A + ~M + 1 - 1 + C = A + ~M + C
        protected override int Operation(ICPURegisters registers, byte operand)
        {
            var carryAddend = registers.GetFlag(StatusRegisterFlags.Carry) ? 1 : 0;
            var xorOperand = (byte)(operand ^ 0xFF); //This inverts M

            ushort sum = (ushort)(registers.GetRegister(Register.Accumulator) + xorOperand + carryAddend);
            var oldAccumulatorValue = registers.GetRegister(Register.Accumulator);
            registers.SetRegister(Register.Accumulator, BytesUtils.GetLoByte(sum));

            //Setup logic operators for Overflow Flag
            bool a = BytesUtils.GetMSB(oldAccumulatorValue); //sign of the accumulator before sum (first addend)
            bool m = BytesUtils.GetMSB(xorOperand); //sign of the xor addend (second addend)
            bool r = BytesUtils.GetMSB(registers.GetRegister(Register.Accumulator)); //sign of the result
            bool v = (a ^ r) & !(a ^ m);

            //This operations sets or unsets the Z, N, C, V flags
            registers.SetFlag(StatusRegisterFlags.Carry, sum > 0x00FF);
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.Accumulator) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, r);
            registers.SetFlag(StatusRegisterFlags.Overflow, v);

            return 0;
        }
    }
}

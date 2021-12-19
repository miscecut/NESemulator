﻿namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    //Compare operation
    public class CompareX : RegistersOperation
    {
        protected override int Operation(CPURegisters registers, byte operand)
        {
            byte subtraction = (byte)((ushort)registers.X - (ushort)operand);

            //this operation sets the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, subtraction == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(subtraction));
            registers.SetFlag(StatusRegisterFlags.Carry, operand <= registers.X);
            return 0;
        }
    }
}
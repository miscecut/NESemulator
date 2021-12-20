using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    //performs an AND between the accumulator and the operand but does not store the result, only sets some flags
    public class Bit : RegistersOperation
    {
        protected override int Operation(ICPURegisters registers, byte operand)
        {
            var andOperationResult = (byte)(registers.GetRegister(Register.Accumulator) & operand);

            //this operation sets/unsets the Z, V, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, andOperationResult == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(operand)); //bit 7 of the operand
            registers.SetFlag(StatusRegisterFlags.Overflow, BytesUtils.GetMSB((byte)(operand << 1))); //bit 6 of the operand
            return 0;
        }
    }
}
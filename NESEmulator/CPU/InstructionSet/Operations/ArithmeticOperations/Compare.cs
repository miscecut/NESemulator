using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    //Compare operation
    public class Compare : RegistersOperation
    {
        private readonly Register _comparedRegister;

        public Compare(Register comparedRegister)
        {
            _comparedRegister = comparedRegister;
        }

        protected override int Operation(ICPURegisters registers, byte operand)
        {
            byte subtraction = (byte)((ushort)registers.GetRegister(_comparedRegister) - (ushort)operand);

            //this operation sets the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, subtraction == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(subtraction));
            registers.SetFlag(StatusRegisterFlags.Carry, operand <= registers.GetRegister(_comparedRegister));

            return 0;
        }
    }
}
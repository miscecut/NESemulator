using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.TransferOperations
{
    public class TransferBetweenRegisters : ImpliedOperation
    {
        private readonly Register _fromRegister;
        private readonly Register _toRegister;

        public TransferBetweenRegisters(Register fromRegister, Register toRegister)
        {
            _fromRegister = fromRegister;
            _toRegister = toRegister;
        }

        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.SetRegister(_toRegister, registers.GetRegister(_fromRegister));

            if(_toRegister == Register.Accumulator) //the flags are checked only in the operations where the accumulator is modified
            {
                //this operation checks the Z & N flags
                registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.Accumulator) == 0x00);
                registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.Accumulator)));
            }

            return 0;
        }
    }
}

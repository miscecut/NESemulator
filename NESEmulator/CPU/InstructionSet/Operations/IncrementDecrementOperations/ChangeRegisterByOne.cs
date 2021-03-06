using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations
{
    public class ChangeRegisterByOne : ImpliedOperation
    {
        private readonly Register _affectedRegister;
        private readonly bool _increment; //if false, it's decrement

        public ChangeRegisterByOne(Register affectedRegister, bool increment)
        {
            _affectedRegister = affectedRegister;
            _increment = increment;
        }

        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            var registerNewValue = _increment ? (byte)(registers.GetRegister(_affectedRegister) + 1) : (byte)(registers.GetRegister(_affectedRegister) - 1);
            registers.SetRegister(_affectedRegister, registerNewValue);

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(_affectedRegister) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(_affectedRegister)));

            return 0;
        }
    }
}

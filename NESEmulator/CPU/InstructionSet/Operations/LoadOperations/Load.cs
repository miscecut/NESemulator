using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.LoadOperations
{
    public class Load : RegistersOperation
    {
        private readonly Register _loadedRegister;

        public Load(Register loadedRegister)
        {
            _loadedRegister = loadedRegister;
        }

        protected override int Operation(ICPURegisters registers, byte operand)
        {
            registers.SetRegister(_loadedRegister, operand);

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(_loadedRegister) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(_loadedRegister)));

            return 0;
        }
    }
}

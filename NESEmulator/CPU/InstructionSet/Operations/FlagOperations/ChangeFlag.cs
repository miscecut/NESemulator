using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class ChangeFlag : IOperation
    {
        private readonly StatusRegisterFlags _flag;
        private readonly bool _set;

        public ChangeFlag(StatusRegisterFlags flag, bool set)
        {
            _flag = flag;
            _set = set;
        }

        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            registers.SetFlag(_flag, _set);
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            registers.SetFlag(_flag, _set);
            return 0;
        }
    }
}

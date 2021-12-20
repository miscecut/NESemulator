using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class ChangeFlag : ImpliedOperation
    {
        private readonly StatusRegisterFlags _flag;
        private readonly bool _set;

        public ChangeFlag(StatusRegisterFlags flag, bool set)
        {
            _flag = flag;
            _set = set;
        }

        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.SetFlag(_flag, _set);
            return 0;
        }
    }
}

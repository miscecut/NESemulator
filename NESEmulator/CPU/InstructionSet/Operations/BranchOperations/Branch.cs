using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.BranchOperations
{
    public class Branch : IOperation
    {
        private readonly StatusRegisterFlags _flagToCheckForBranch; //which flag to check for branching?
        private readonly bool _branchIf; //has the flag to be set or unset?

        public Branch(StatusRegisterFlags flag, bool set)
        {
            _flagToCheckForBranch = flag;
            _branchIf = set;
        }

        public int OperationImmediate(IBus bus, ICPURegisters registers) //this method shouldn't be called
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address) //this is the effective operation, the address passed is the result of the Relative Address Mode
        {
            var additionalCycles = 0;

            if(registers.GetFlag(_flagToCheckForBranch) == _branchIf)
            {
                additionalCycles++; //a new cycle is required if the branch is taken

                var newProgramCounter = (ushort)(address + registers.GetProgramCounter()); //a new program counter is set, this is where the program will branch
                if (BytesUtils.GetHiByte(newProgramCounter) != BytesUtils.GetHiByte(registers.GetProgramCounter()))
                    additionalCycles++; //if a page is crossed, an additional cycle is required
                registers.SetProgramCounter(newProgramCounter);
            }

            return additionalCycles;
        }
    }
}

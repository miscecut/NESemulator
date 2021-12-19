using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class Jump : IOperation
    {
        private readonly bool _pushProgramCounter; //if this is true, the PC is pushed to the stack (JMP / JSR difference)

        public Jump(bool pushProgramCounter)
        {
            _pushProgramCounter = pushProgramCounter;
        }

        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            if (_pushProgramCounter)
            {
                registers.ProgramCounter--;
                //the program counter is pushed to the stack
                bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), BytesUtils.GetHiByte(registers.ProgramCounter));
                bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), BytesUtils.GetLoByte(registers.ProgramCounter));
            }

            registers.ProgramCounter = address; //the program counter becomes the address provided

            return 0;
        }
    }
}

using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class Jump : IOperation
    {
        private readonly bool _pushProgramCounter; //if this is true, the PC is pushed to the stack (JMP / JSR difference)

        public Jump(bool pushProgramCounter)
        {
            _pushProgramCounter = pushProgramCounter;
        }

        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            if (_pushProgramCounter)
            {
                registers.DecrementProgramCounter();
                //the program counter is pushed to the stack
                bus.CPUWrite((ushort)(0x0100 + registers.GetRegister(Register.StackPointer)), BytesUtils.GetHiByte(registers.GetProgramCounter()));
                registers.DecrementStackPointer();
                bus.CPUWrite((ushort)(0x0100 + registers.GetRegister(Register.StackPointer)), BytesUtils.GetLoByte(registers.GetProgramCounter()));
                registers.DecrementStackPointer();
            }

            registers.SetProgramCounter(address); //the program counter becomes the address provided

            return 0;
        }
    }
}

using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class Break : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            //First, it increments the program counter
            registers.ProgramCounter++;
            //It sets the I flag to true
            registers.SetFlag(StatusRegisterFlags.IRQDisable, true);
            //and it saves the program counter on the stack
            var loProgramCounter = BytesUtils.GetLoByte(registers.ProgramCounter);
            var hiProgramCounter = BytesUtils.GetHiByte(registers.ProgramCounter);
            bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), loProgramCounter); //save the loPointer to the stack
            bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), hiProgramCounter); //save the hiPointer to the stack
            //It sets the B flag to true
            registers.SetFlag(StatusRegisterFlags.BRKCommand, true);
            //And it pushes the status register on the stack
            bus.CPUWrite((ushort)(0x0100 + registers.StackPointer--), registers.Status); //save the hiPointer to the stack
            //And it resets the B flag to false
            registers.SetFlag(StatusRegisterFlags.BRKCommand, false);
            //It finally updated the program counter
            var loNewProgramCounter = bus.CPURead(0xFFFE);
            var hiNewProgramCounter = bus.CPURead(0xFFFF);
            registers.ProgramCounter = BytesUtils.CombineBytes(hiNewProgramCounter, loNewProgramCounter);

            return 0; //it does not require additional cycles
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

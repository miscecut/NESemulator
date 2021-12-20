using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class Break : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            //First, it increments the program counter
            registers.IncrementProgramCounter();
            //It sets the I flag to true
            registers.SetFlag(StatusRegisterFlags.IRQDisable, true);
            //and it saves the program counter on the stack
            var loProgramCounter = BytesUtils.GetLoByte(registers.GetProgramCounter());
            var hiProgramCounter = BytesUtils.GetHiByte(registers.GetProgramCounter());
            bus.CPUWrite((ushort)(0x0100 + registers.GetStackPointer()), loProgramCounter); //save the loPointer to the stack
            registers.DecrementStackPointer();
            bus.CPUWrite((ushort)(0x0100 + registers.GetStackPointer()), hiProgramCounter); //save the hiPointer to the stack
            registers.DecrementStackPointer();
            //It sets the B flag to true
            registers.SetFlag(StatusRegisterFlags.BRKCommand, true);
            //And it pushes the status register on the stack
            bus.CPUWrite((ushort)(0x0100 + registers.GetStackPointer()), registers.GetStatus()); //save the hiPointer to the stack
            registers.DecrementStackPointer();
            //And it resets the B flag to false
            registers.SetFlag(StatusRegisterFlags.BRKCommand, false);
            //It finally updated the program counter
            var loNewProgramCounter = bus.CPURead(0xFFFE);
            var hiNewProgramCounter = bus.CPURead(0xFFFF);
            registers.SetProgramCounter(BytesUtils.CombineBytes(hiNewProgramCounter, loNewProgramCounter));

            return 0; //it does not require additional cycles
        }
    }
}

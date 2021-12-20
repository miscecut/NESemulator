using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class ReturnFromSubRoutine : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            var loProgramCounter = bus.CPURead((ushort)(registers.GetRegister(Register.StackPointer) + 0x0100));
            registers.IncrementStackPointer();
            var hiProgramCounter = bus.CPURead((ushort)(registers.GetRegister(Register.StackPointer) + 0x0100));
            registers.SetProgramCounter(BytesUtils.CombineBytes(hiProgramCounter, loProgramCounter));
            registers.IncrementProgramCounter();
            return 0;
        }
    }
}

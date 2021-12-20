using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class ReturnFromInterrupt : ImpliedOperation
    {
        protected override int OperationImplied(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            var statusBeforeInterrupt = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            registers.IncrementStackPointer();
            var loProgramCounter = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            registers.IncrementStackPointer();
            var hiProgramCounter = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            var programCounter = BytesUtils.CombineBytes(hiProgramCounter, loProgramCounter);

            registers.SetStatus(statusBeforeInterrupt);
            registers.SetProgramCounter(programCounter);
            registers.SetFlag(StatusRegisterFlags.BRKCommand, false);

            return 0;
        }
    }
}

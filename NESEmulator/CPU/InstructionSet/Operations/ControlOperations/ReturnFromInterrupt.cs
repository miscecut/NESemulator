using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class ReturnFromInterrupt : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            var statusBeforeInterrupt = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            var loProgramCounter = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            var hiProgramCounter = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            var programCounter = BytesUtils.CombineBytes(hiProgramCounter, loProgramCounter);

            registers.Status = statusBeforeInterrupt;
            registers.ProgramCounter = programCounter;
            registers.SetFlag(StatusRegisterFlags.BRKCommand, false);

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

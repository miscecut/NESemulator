using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class ReturnFromSubRoutine : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            registers.IncrementStackPointer();
            var loProgramCounter = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            registers.IncrementStackPointer();
            var hiProgramCounter = bus.CPURead((ushort)(registers.GetStackPointer() + 0x0100));
            registers.SetProgramCounter(BytesUtils.CombineBytes(hiProgramCounter, loProgramCounter));
            registers.IncrementProgramCounter();
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

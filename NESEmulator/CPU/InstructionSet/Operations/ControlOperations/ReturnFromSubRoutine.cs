using NESEmulator.Bus;
using System;

namespace NESEmulator.CPU.InstructionSet.Operations.ControlOperations
{
    public class ReturnFromSubRoutine : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            var loProgramCounter = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            var hiProgramCounter = bus.CPURead((ushort)(++registers.StackPointer + 0x0100));
            registers.ProgramCounter = BytesUtils.CombineBytes(hiProgramCounter, loProgramCounter);
            registers.ProgramCounter++;
            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            return 0;
        }
    }
}

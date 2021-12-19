using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations
{
    //operation that affect only the registers and doesn't write on memory
    public abstract class RegistersOperation : IOperation
    {
        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            var operand = bus.CPURead(registers.GetProgramCounterAndIncrement());
            return Operation(registers, operand);
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            var operand = bus.CPURead(address);
            return Operation(registers, operand);
        }

        protected abstract int Operation(CPURegisters registers, byte operand);
    }
}

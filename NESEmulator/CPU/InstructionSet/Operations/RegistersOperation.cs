using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations
{
    //operation that affect only the registers and doesn't write on memory
    public abstract class RegistersOperation : IOperation
    {
        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            var operand = bus.CPURead(registers.GetProgramCounterAndIncrement());
            return Operation(registers, operand);
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            var operand = bus.CPURead(address);
            return Operation(registers, operand);
        }

        protected abstract int Operation(ICPURegisters registers, byte operand);
    }
}

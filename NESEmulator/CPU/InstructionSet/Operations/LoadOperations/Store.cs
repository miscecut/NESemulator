using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.LoadOperations
{
    public class Store : IOperation
    {
        private readonly Register _storedRegister;

        public Store(Register storedRegister)
        {
            _storedRegister = storedRegister;
        }

        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            bus.CPUWrite(address, registers.GetRegister(_storedRegister));

            return 0;
        }
    }
}

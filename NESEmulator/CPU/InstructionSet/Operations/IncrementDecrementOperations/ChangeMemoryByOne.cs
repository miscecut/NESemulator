using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations
{
    public class ChangeMemoryByOne : IOperation //TODO renderla capace di gestire anche il decrement
    {
        private readonly bool _increment; //if false, it's decrement

        public ChangeMemoryByOne(bool increment)
        {
            _increment = increment;
        }

        public int OperationImmediate(IBus bus, ICPURegisters registers) //it should be impossibile, an address has to be provided
        {
            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
        {
            var oldValue = bus.CPURead(address);
            var newValue = _increment ? (byte)(oldValue + 1) : (byte)(oldValue - 1);
            bus.CPUWrite(address, newValue);

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, newValue == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(newValue));

            return 0;
        }
    }
}

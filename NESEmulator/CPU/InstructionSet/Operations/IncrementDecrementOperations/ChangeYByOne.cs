using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations
{
    public class ChangeYByOne : IOperation //TODO renderla capace di gestire anche il decrement
    {
        private readonly bool _increment; //if false, it's decrement

        public ChangeYByOne(bool increment)
        {
            _increment = increment;
        }

        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            registers.Y = _increment ? (byte)(registers.Y + 1) : (byte)(registers.Y - 1);

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.Y == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.Y));

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address) //it should be impossibile, an address has not to be provided
        {
            //TODO: mettere qui il codice di ChangeMemoryByOne?
            return 0;
        }
    }
}

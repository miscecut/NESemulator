using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations
{
    public class ChangeXByOne : IOperation //TODO renderla capace di gestire anche il decrement
    {
        private readonly bool _increment; //if false, it's decrement

        public ChangeXByOne(bool increment)
        {
            _increment = increment;
        }

        public int OperationImmediate(IBus bus, CPURegisters registers) 
        {
            registers.X = _increment ? (byte)(registers.X + 1) : (byte)(registers.X - 1);

            //This operations sets or unsets the Z, N flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.X == 0x00);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.X));

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address) //it should be impossibile, an address has not to be provided
        {
            return 0;
        }
    }
}

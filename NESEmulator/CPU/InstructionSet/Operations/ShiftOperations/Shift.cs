using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.Operations.OperationImplementation
{
    public class Shift : IOperation
    {
        private readonly bool _left; //if true, the shift is to the left, else is to the right
        private readonly bool _rotate; //if true, the carry bit of the status is taken in consideration when performing the shift

        public Shift(bool left, bool rotate)
        {
            _left = left;
            _rotate = rotate;
        }

        public int OperationImmediate(IBus bus, CPURegisters registers)
        {
            bool carry;
            if (_left)
            {
                carry = BytesUtils.GetMSB(registers.A);
                registers.A = (byte)(registers.A << 1);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    registers.A += 0b00000001;
            }
            else
            {
                carry = BytesUtils.GetLSB(registers.A);
                registers.A = (byte)(registers.A >> 1);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    registers.A += 0b10000000;
            }

            //this operation checks the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.A == 0x00);
            registers.SetFlag(StatusRegisterFlags.Carry, carry);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.A));

            return 0;
        }

        public int OperationWithAddress(IBus bus, CPURegisters registers, ushort address)
        {
            var operand = bus.CPURead(address);

            bool carry;
            byte shiftedOperand = 0x00;
            if (_left)
            {
                carry = BytesUtils.GetMSB(operand);
                shiftedOperand = (byte)(operand << 1);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    shiftedOperand += 0b00000001;
            }
            else
            {
                carry = BytesUtils.GetLSB(operand);
                shiftedOperand = (byte)(operand >> 1);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    shiftedOperand += 0b10000000;
            }
            bus.CPUWrite(address, shiftedOperand);

            //this operation checks the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, shiftedOperand == 0x00);
            registers.SetFlag(StatusRegisterFlags.Carry, carry);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(shiftedOperand));

            return 0;
        }
    }
}

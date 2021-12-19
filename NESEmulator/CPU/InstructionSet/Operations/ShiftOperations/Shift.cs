using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

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

        public int OperationImmediate(IBus bus, ICPURegisters registers)
        {
            bool carry;
            if (_left)
            {
                carry = BytesUtils.GetMSB(registers.GetRegister(Register.A));
                var newAccumulatorValue = (byte)(registers.GetRegister(Register.A) << 1);
                registers.SetRegister(Register.A, newAccumulatorValue);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    registers.IncrementRegister(Register.A);
            }
            else
            {
                carry = BytesUtils.GetLSB(registers.GetRegister(Register.A));
                var newAccumulatorValue = (byte)(registers.GetRegister(Register.A) >> 1);
                if (_rotate && registers.GetFlag(StatusRegisterFlags.Carry))
                    registers.SetRegister(Register.A, (byte)(registers.GetRegister(Register.A) + 0b10000000));
            }

            //this operation checks the Z, N, C flags
            registers.SetFlag(StatusRegisterFlags.Zero, registers.GetRegister(Register.A) == 0x00);
            registers.SetFlag(StatusRegisterFlags.Carry, carry);
            registers.SetFlag(StatusRegisterFlags.Negative, BytesUtils.GetMSB(registers.GetRegister(Register.A)));

            return 0;
        }

        public int OperationWithAddress(IBus bus, ICPURegisters registers, ushort address)
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

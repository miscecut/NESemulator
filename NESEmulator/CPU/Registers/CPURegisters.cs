using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU
{
    public class CPURegisters : ICPURegisters
    {
        private byte A;
        private byte X;
        private byte Y;
        private ushort ProgramCounter;
        private byte StackPointer;
        private byte Status;

        public CPURegisters()
        {
            Reset();
        }

        public void Reset()
        {
            A = 0x00;
            X = 0x00;
            Y = 0x00;
            ProgramCounter = 0x0000;
            StackPointer = 0xFD;
            Status = 0b00100000; //UNUSED flag is always set
        }

        public ushort GetProgramCounterAndIncrement()
        {
            return ProgramCounter++;
        }

        public void IncrementProgramCounter()
        {
            ProgramCounter++;
        }

        public bool GetFlag(StatusRegisterFlags flag)
        {
            return (Status / ((byte)flag)) % 2 == 1;
        }

        public void SetFlag(StatusRegisterFlags flag, bool set)
        {
            if (set)
                Status = (byte)(Status | (byte)flag);
            else
                Status = (byte)(Status & (byte)~flag);
        }

        public byte GetRegister(Register register)
        {
            if (register == Register.Accumulator)
                return A;
            if (register == Register.X)
                return X;
            if (register == Register.Y)
                return Y;
            return StackPointer;
        }

        public void SetRegister(Register register, byte value)
        {
            if (register == Register.Accumulator)
                A = value;
            else if (register == Register.X)
                X = value;
            else if (register == Register.Y)
                Y = value;
            else
                StackPointer = value;
        }

        public ushort GetProgramCounter()
        {
            return ProgramCounter;
        }

        public void SetProgramCounter(ushort programCounter)
        {
            ProgramCounter = programCounter;
        }

        public void IncrementStackPointer()
        {
            StackPointer++;
        }

        public void DecrementStackPointer()
        {
            StackPointer--;
        }

        public byte GetStatus()
        {
            return Status;
        }

        public void SetStatus(byte status)
        {
            Status = status;
        }

        public void DecrementProgramCounter()
        {
            ProgramCounter--;
        }

        public void IncrementRegister(Register register)
        {
            if (register == Register.Accumulator)
                A++;
            if (register == Register.X)
                X++;
            Y++;
        }
    }
}

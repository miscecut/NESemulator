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
            if (register == Register.A)
                return A;
            if (register == Register.X)
                return X;
            return Y;
        }

        public void SetRegister(Register register, byte value)
        {
            if (register == Register.A)
                A = value;
            if (register == Register.X)
                X = value;
            Y = value;
        }

        public ushort GetProgramCounter()
        {
            return ProgramCounter;
        }

        public void SetProgramCounter(ushort programCounter)
        {
            ProgramCounter = programCounter;
        }

        public byte GetStackPointer()
        {
            return StackPointer;
        }

        public void SetStackPointer(byte stackPointer)
        {
            StackPointer = stackPointer;
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
            if (register == Register.A)
                A++;
            if (register == Register.X)
                X++;
            Y++;
        }
    }
}

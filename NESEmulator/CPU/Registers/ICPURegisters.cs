namespace NESEmulator.CPU.Registers
{
    public interface ICPURegisters
    {
        public byte GetRegister(Register register);
        public void SetRegister(Register register, byte value);
        public void IncrementRegister(Register register);
        public byte GetStatus();
        public void SetStatus(byte status);
        public byte GetStackPointer();
        public void SetStackPointer(byte stackPointer);
        public void IncrementStackPointer();
        public void DecrementStackPointer();
        public ushort GetProgramCounter();
        public void SetProgramCounter(ushort programCounter);
        public ushort GetProgramCounterAndIncrement();
        public void IncrementProgramCounter();
        public void DecrementProgramCounter();
        public bool GetFlag(StatusRegisterFlags flag);
        public void SetFlag(StatusRegisterFlags flag, bool set);
        public void Reset();

    }
}

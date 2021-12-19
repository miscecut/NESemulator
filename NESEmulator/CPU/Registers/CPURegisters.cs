namespace NESEmulator.CPU
{
    public class CPURegisters
    {
        //Accumulator (1 Byte)
        public byte A { get; set; }
        //X Register (1 Byte)
        public byte X { get; set; }
        //Y Register (1 Byte)
        public byte Y { get; set; }
        //Program counter (2 Bytes)
        public ushort ProgramCounter { get; set; }
        //Stack pointer (1 Byte)
        public byte StackPointer { get; set; }
        //P Register: 8 flags
        public byte Status { get; set; }

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
    }
}

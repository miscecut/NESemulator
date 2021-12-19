namespace NESEmulator.CPU
{
    public enum StatusRegisterFlags
    {
        Negative = 0b10000000,
        Overflow = 0b01000000,
        Unused = 0b00100000,
        BRKCommand = 0b00010000,
        DecimalMode = 0b00001000,
        IRQDisable = 0b00000100,
        Zero = 0b00000010,
        Carry = 0b00000001
    }
}

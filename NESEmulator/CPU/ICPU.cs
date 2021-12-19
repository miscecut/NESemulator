namespace NESEmulator.CPU
{
    public interface ICPU
    {
        public void Clock();
        public void Reset();
        public void NMI();
        public void IRQ();
    }
}

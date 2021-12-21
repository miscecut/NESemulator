namespace NESEmulator.CPU
{
    public interface ICPU
    {
        public void Clock();
        public void Reset();
        public void NonMaskeableInterrupt();
        public void InterruptRequest();
    }
}

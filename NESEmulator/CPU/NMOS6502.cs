using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU
{
    public class NMOS6502 : ICPU
    {
        private readonly ICPURegisters _registers;
        private readonly IBus _bus;
        private readonly CPUInstructionSet _instructionSet;
        private int _remainingCycles;

        public NMOS6502(IBus bus, ICPURegisters registers)
        {
            _bus = bus;
            _registers = registers;
            _instructionSet = new CPUInstructionSet();
            _remainingCycles = 0;
        }

        public void Clock()
        {
            if(_remainingCycles == 0)
            {
                _registers.SetFlag(StatusRegisterFlags.Unused, true);

                var programCounter = _registers.GetProgramCounterAndIncrement();
                var opcode = _bus.CPURead(programCounter);
                var instruction = _instructionSet.GetInstruction(opcode);
                _remainingCycles = instruction.Execute(_bus, _registers);

                _registers.SetFlag(StatusRegisterFlags.Unused, true);
            }
            _remainingCycles -= 1;
        }

        public void IRQ()
        {
            throw new System.NotImplementedException();
        }

        public void NMI()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            _remainingCycles = 8;
            _registers.Reset();
            var loAddress = _bus.CPURead(0xFFFC);
            var hiAddress = _bus.CPURead(0xFFFD);
            _registers.SetProgramCounter(BytesUtils.CombineBytes(hiAddress, loAddress));
        }
    }
}

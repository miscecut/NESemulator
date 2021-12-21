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

        public void InterruptRequest()
        {
            if (!_registers.GetFlag(StatusRegisterFlags.IRQDisable))
            {
                _remainingCycles = 7;
                HandleInterrupt(0xFFFE);
            }
        }

        public void NonMaskeableInterrupt()
        {
            _remainingCycles = 8;
            HandleInterrupt(0xFFFA);
        }

        public void Reset()
        {
            _remainingCycles = 8;
            _registers.Reset();
            var loAddress = _bus.CPURead(0xFFFC);
            var hiAddress = _bus.CPURead(0xFFFD);
            _registers.SetProgramCounter(BytesUtils.CombineBytes(hiAddress, loAddress));
        }

        private void HandleInterrupt(ushort newProgramCounterAddress)
        {
            //First, it writes the current program counter to the stack
            var currentProgramCounter = _registers.GetProgramCounter();
            _bus.CPUWrite((ushort)(0x0100 + _registers.GetRegister(Register.StackPointer)), BytesUtils.GetHiByte(currentProgramCounter));
            _registers.DecrementStackPointer();
            _bus.CPUWrite((ushort)(0x0100 + _registers.GetRegister(Register.StackPointer)), BytesUtils.GetLoByte(currentProgramCounter));
            _registers.DecrementStackPointer();

            //Then it saves the status register on the stack
            _registers.SetFlag(StatusRegisterFlags.BRKCommand, false);
            _registers.SetFlag(StatusRegisterFlags.Unused, true);
            _registers.SetFlag(StatusRegisterFlags.IRQDisable, true); //It indicates that an interrupt has occurred
            _bus.CPUWrite((ushort)(0x0100 + _registers.GetRegister(Register.StackPointer)), _registers.GetStatus());
            _registers.DecrementStackPointer();

            //Finally, it forces the program counter to jump to a know location in the memory
            var loAddress = _bus.CPURead(newProgramCounterAddress);
            var hiAddress = _bus.CPURead((ushort)(newProgramCounterAddress + 1));
            _registers.SetProgramCounter(BytesUtils.CombineBytes(hiAddress, loAddress));
        }
    }
}

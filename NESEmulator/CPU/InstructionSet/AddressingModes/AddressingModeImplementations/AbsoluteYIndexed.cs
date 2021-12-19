using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class AbsoluteYIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var loAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var hiAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var address = (ushort)(BytesUtils.CombineBytes(hiAddress, loAddress) + registers.GetRegister(Register.Y));
            
            var additionalCycles = 0;
            if (hiAddress != BytesUtils.GetHiByte(address)) //if a page boundary is crossed, the operation requires an additional clock cycle
                additionalCycles++;

            return new AddressingModeResult
            {
                AdditionalCycles = additionalCycles,
                Address = address
            };
        }
    }
}

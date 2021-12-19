using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class IndirectYIndexed : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loPointerLoAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var loPointerAddress = BytesUtils.CombineBytes(0x00, loPointerLoAddress);
            var hiPointerAddress = BytesUtils.CombineBytes(0x00, (byte)(loPointerLoAddress + 1));

            var loPointerBeforeSum = bus.CPURead(loPointerAddress);
            var hiPointerBeforeSum = bus.CPURead(hiPointerAddress);
            var pointerBeforeSum = BytesUtils.CombineBytes(hiPointerBeforeSum, loPointerBeforeSum);
            var pointer = (ushort)(pointerBeforeSum + registers.Y);

            var additionalCycles = 0;
            if (hiPointerBeforeSum != BytesUtils.GetHiByte(pointer)) //if a page boundary is crossed, the operation requires an additional clock cycle
                additionalCycles++;

            return new AddressingModeResult
            {
                AdditionalCycles = additionalCycles,
                Address = pointer
            };
        }
    }
}

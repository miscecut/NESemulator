using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class IndexedXIndirect : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loPointerloAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var loPointerAddress = BytesUtils.ZeroPageSum(loPointerloAddress, registers.X);
            var hiPointerAddress = BytesUtils.ZeroPageSum(loPointerloAddress, (byte)(registers.X + 1));

            var loPointer = bus.CPURead(loPointerAddress);
            var hiPointer = bus.CPURead(hiPointerAddress);
            var pointer = BytesUtils.CombineBytes(hiPointer, loPointer);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = pointer
            };
        }
    }
}

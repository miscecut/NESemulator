using NESEmulator.Bus;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class IndexedXIndirect : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, ICPURegisters registers)
        {
            var xValue = registers.GetRegister(Register.X);
            var loPointerloAddress = bus.CPURead(registers.GetProgramCounterAndIncrement());
            ushort loPointerAddress = BytesUtils.ZeroPageSum(loPointerloAddress, xValue);
            var hiPointerAddress = BytesUtils.ZeroPageSum(loPointerloAddress, (byte)(xValue + 1));

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

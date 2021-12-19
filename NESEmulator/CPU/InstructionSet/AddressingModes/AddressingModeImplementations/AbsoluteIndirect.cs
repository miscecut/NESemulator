using NESEmulator.Bus;

namespace NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations
{
    public class AbsoluteIndirect : IAddressingMode
    {
        public AddressingModeResult Fetch(IBus bus, CPURegisters registers)
        {
            var loAddressPointer = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var hiAddressPointer = bus.CPURead(registers.GetProgramCounterAndIncrement());
            var addressPointer = BytesUtils.CombineBytes(hiAddressPointer, loAddressPointer);
            
            //6502 hardware's bug
            var hiAddress = loAddressPointer == 0xFF ? bus.CPURead(BytesUtils.CombineBytes(hiAddressPointer, 0x00)) /*this is the bug*/ : bus.CPURead((ushort)(addressPointer + 1));
            var loAddress = bus.CPURead(addressPointer);
            var pointer = BytesUtils.CombineBytes(hiAddress, loAddress);

            return new AddressingModeResult
            {
                AdditionalCycles = 0,
                Address = pointer
            };
        }
    }
}

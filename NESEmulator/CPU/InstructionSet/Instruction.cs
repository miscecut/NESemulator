using NESEmulator.Bus;
using NESEmulator.CPU.InstructionSet.AddressingModes;
using NESEmulator.CPU.InstructionSet.Operations;
using NESEmulator.CPU.Registers;

namespace NESEmulator.CPU
{
    public class Instruction
    {
        //how does the instruction fetch the address (if it needs it)?
        public IAddressingMode? AddressingMode { get; set; } //if not set, it's immediate
        //What does the instruction do?
        public IOperation Operation { get; set; }
        //What is the opcode of this operation?
        public byte Opcode { get; set; }
        //What is the code name of this operation?
        public string Name { get; set; }
        //How many clock cycles are needed to do this instruction?
        public int RequiredClockCycles { get; set; }
        
        public int Execute(IBus bus, ICPURegisters registers)
        {
            if(AddressingMode is null)
                return RequiredClockCycles + Operation.OperationImmediate(bus, registers);
            else
            {
                var address = AddressingMode.Fetch(bus, registers);
                return RequiredClockCycles + address.AdditionalCycles + Operation.OperationWithAddress(bus, registers, address.Address);
            }
        }
    }
}
using NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations;
using NESEmulator.CPU.InstructionSet.Operations.BranchOperations;
using NESEmulator.CPU.InstructionSet.Operations.ControlOperations;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;
using System.Collections.Generic;

namespace NESEmulator.CPU
{
    public class CPUInstructionSet
    {
        //A map opcode -> instruction (every instruction has a different opcode which identifies it)
        private readonly IDictionary<byte, Instruction> _instructions;

        public CPUInstructionSet()
        {
            _instructions = new Dictionary<byte, Instruction>();
            //The instructions are in opcode-order
            _instructions[0x00] = new Instruction
            {
                Opcode = 0x00,
                Name = "BRK",
                RequiredClockCycles = 7,
                Operation = new Break()
            };
            _instructions[0x01] = new Instruction
            {
                Opcode = 0x01,
                Name = "ORA ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new Or()
            };
            _instructions[0x05] = new Instruction
            {
                Opcode = 0x05,
                Name = "ORA $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Or()
            };
            _instructions[0x06] = new Instruction
            {
                Opcode = 0x06,
                Name = "ASL $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new Shift(true, false)
            };
            _instructions[0x08] = new Instruction
            {
                Opcode = 0x08,
                Name = "PHP",
                RequiredClockCycles = 3,
                Operation = new PushStatusOnStack()
            };
            _instructions[0x09] = new Instruction
            {
                Opcode = 0x09,
                Name = "ORA #$aa",
                RequiredClockCycles = 2,
                Operation = new Or()
            };
            _instructions[0x0A] = new Instruction
            {
                Opcode = 0x0A,
                Name = "ASL A",
                RequiredClockCycles = 2,
                Operation = new Shift(true, false)
            };
            _instructions[0x0D] = new Instruction
            {
                Opcode = 0x0D,
                Name = "ORA $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Or()
            };
            _instructions[0x0E] = new Instruction
            {
                Opcode = 0x0E,
                Name = "ASL $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new Shift(true, false)
            };
            _instructions[0x10] = new Instruction
            {
                Opcode = 0x10,
                Name = "BPL",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Negative, false)
            };
            _instructions[0x11] = new Instruction
            {
                Opcode = 0x11,
                Name = "ORA (oper),Y",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new Or()
            };
            _instructions[0x15] = new Instruction
            {
                Opcode = 0x15,
                Name = "ORA $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Or()
            };
            _instructions[0x16] = new Instruction
            {
                Opcode = 0x16,
                Name = "ASL $aa,X",
                RequiredClockCycles = 6,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Shift(true, false)
            };
            _instructions[0x18] = new Instruction
            {
                Opcode = 0x18,
                Name = "CLC",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.Carry, false)
            };
            _instructions[0x19] = new Instruction
            {
                Opcode = 0x19,
                Name = "ORA oper,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Or()
            };
            _instructions[0x1D] = new Instruction
            {
                Opcode = 0x1D,
                Name = "ORA $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Or()
            };
            _instructions[0x1E] = new Instruction
            {
                Opcode = 0x1E,
                Name = "ASL $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Shift(true, false)
            };
            _instructions[0x20] = new Instruction
            {
                Opcode = 0x20,
                Name = "JSR $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new Jump(true)
            };
            _instructions[0x21] = new Instruction
            {
                Opcode = 0x21,
                Name = "AND ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new And()
            };
            _instructions[0x24] = new Instruction
            {
                Opcode = 0x24,
                Name = "BIT $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Bit()
            };
            _instructions[0x25] = new Instruction
            {
                Opcode = 0x25,
                Name = "AND $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new And()
            };
            _instructions[0x26] = new Instruction
            {
                Opcode = 0x26,
                Name = "ROL $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new Shift(false, true)
            };
            _instructions[0x28] = new Instruction
            {
                Opcode = 0x28,
                Name = "PLP",
                RequiredClockCycles = 4,
                Operation = new PullStatusFromStack()
            };
            _instructions[0x29] = new Instruction
            {
                Opcode = 0x29,
                Name = "AND #$aa",
                RequiredClockCycles = 2,
                Operation = new And()
            };
            _instructions[0x2A] = new Instruction
            {
                Opcode = 0x2A,
                Name = "ROL A",
                RequiredClockCycles = 2,
                Operation = new Shift(false, true)
            };
            _instructions[0x2C] = new Instruction
            {
                Opcode = 0x2C,
                Name = "BIT $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Bit()
            };
            _instructions[0x2D] = new Instruction
            {
                Opcode = 0x2D,
                Name = "AND $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new And()
            };
            _instructions[0x2E] = new Instruction
            {
                Opcode = 0x2E,
                Name = "ROL $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new Shift(false, true)
            };
            _instructions[0x30] = new Instruction
            {
                Opcode = 0x30,
                Name = "BMI $aaaa",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Negative, true)
            };
            _instructions[0x31] = new Instruction
            {
                Opcode = 0x31,
                Name = "AND ($aa,Y)",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new And()
            };
            _instructions[0x35] = new Instruction
            {
                Opcode = 0x35,
                Name = "AND $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new And()
            };
            _instructions[0x36] = new Instruction
            {
                Opcode = 0x36,
                Name = "ROL $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Shift(false, true)
            };
            _instructions[0x38] = new Instruction
            {
                Opcode = 0x38,
                Name = "SEC",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.Carry, true)
            };
            _instructions[0x39] = new Instruction
            {
                Opcode = 0x39,
                Name = "AND $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new And()
            };
            _instructions[0x3D] = new Instruction
            {
                Opcode = 0x3D,
                Name = "AND $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new And()
            };
            _instructions[0x3E] = new Instruction
            {
                Opcode = 0x3E,
                Name = "ROL $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Shift(false, true)
            };
            _instructions[0x40] = new Instruction
            {
                Opcode = 0x40,
                Name = "RTI",
                RequiredClockCycles = 6,
                Operation = new ReturnFromInterrupt()
            };
            _instructions[0x41] = new Instruction
            {
                Opcode = 0x41,
                Name = "EOR ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new Xor()
            };
            _instructions[0x45] = new Instruction
            {
                Opcode = 0x45,
                Name = "EOR $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Xor()
            };
            _instructions[0x46] = new Instruction
            {
                Opcode = 0x46,
                Name = "LSR $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new Shift(false, false)
            };
            _instructions[0x48] = new Instruction
            {
                Opcode = 0x48,
                Name = "PHA",
                RequiredClockCycles = 3,
                Operation = new PushAccumulatorOnStack()
            };
            _instructions[0x49] = new Instruction
            {
                Opcode = 0x49,
                Name = "EOR #$aa",
                RequiredClockCycles = 2,
                Operation = new Xor()
            };
            _instructions[0x4A] = new Instruction
            {
                Opcode = 0x4A,
                Name = "LSR A",
                RequiredClockCycles = 2,
                Operation = new Shift(false, false)
            };
            _instructions[0x4C] = new Instruction
            {
                Opcode = 0x4C,
                Name = "JMP $aaaa",
                RequiredClockCycles = 3,
                AddressingMode = new Absolute(),
                Operation = new Jump(false)
            };
            _instructions[0x4D] = new Instruction
            {
                Opcode = 0x4D,
                Name = "EOR $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Xor()
            };
            _instructions[0x4E] = new Instruction
            {
                Opcode = 0x4E,
                Name = "LSR $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new Shift(false, false)
            };
            _instructions[0x50] = new Instruction
            {
                Opcode = 0x50,
                Name = "BVC",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Overflow, true)
            };
            _instructions[0x51] = new Instruction
            {
                Opcode = 0x51,
                Name = "EOR ($aa,Y)",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new Xor()
            };
            _instructions[0x55] = new Instruction
            {
                Opcode = 0x55,
                Name = "EOR $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Xor()
            };
            _instructions[0x56] = new Instruction
            {
                Opcode = 0x56,
                Name = "LSR $aa,X",
                RequiredClockCycles = 6,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Shift(false, false)
            };
            _instructions[0x58] = new Instruction
            {
                Opcode = 0x58,
                Name = "CLI",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.IRQDisable, false)
            };
            _instructions[0x59] = new Instruction
            {
                Opcode = 0x59,
                Name = "EOR $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Xor()
            };
            _instructions[0x5D] = new Instruction
            {
                Opcode = 0x5D,
                Name = "EOR $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Xor()
            };
            _instructions[0x5E] = new Instruction
            {
                Opcode = 0x5E,
                Name = "LSR $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Shift(false, false)
            };
            _instructions[0x60] = new Instruction
            {
                Opcode = 0x60,
                Name = "RTS",
                RequiredClockCycles = 6,
                Operation = new ReturnFromSubRoutine()
            };
            _instructions[0x61] = new Instruction
            {
                Opcode = 0x61,
                Name = "ADC $aaaa,X",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new AddWithCarry()
            };
            _instructions[0x65] = new Instruction
            {
                Opcode = 0x65,
                Name = "ADC $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new AddWithCarry()
            };
            _instructions[0x66] = new Instruction
            {
                Opcode = 0x66,
                Name = "ROR $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new Shift(false, true)
            };
            _instructions[0x68] = new Instruction
            {
                Opcode = 0x68,
                Name = "PLA",
                RequiredClockCycles = 4,
                Operation = new PullAccumulatorFromStack()
            };
            _instructions[0x69] = new Instruction
            {
                Opcode = 0x69,
                Name = "ADC #$aa",
                RequiredClockCycles = 4,
                Operation = new AddWithCarry()
            };
            _instructions[0x6A] = new Instruction
            {
                Opcode = 0x6A,
                Name = "ROR A",
                RequiredClockCycles = 2,
                Operation = new Shift(false, true)
            };
            _instructions[0x6C] = new Instruction
            {
                Opcode = 0x6C,
                Name = "JMP ($aaaa)",
                RequiredClockCycles = 5,
                AddressingMode = new AbsoluteIndirect(),
                Operation = new Jump(false)
            };
            _instructions[0x6D] = new Instruction
            {
                Opcode = 0x6D,
                Name = "ADC $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new AddWithCarry()
            };
            _instructions[0x6E] = new Instruction
            {
                Opcode = 0x6E,
                Name = "ROR $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new Shift(false, true)
            };
            _instructions[0x70] = new Instruction
            {
                Opcode = 0x70,
                Name = "BVS",
                RequiredClockCycles = 2,
                Operation = new Branch(StatusRegisterFlags.Overflow, true)
            };
            _instructions[0x71] = new Instruction
            {
                Opcode = 0x71,
                Name = "ADC ($aa),Y",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new AddWithCarry()
            };
            _instructions[0x75] = new Instruction
            {
                Opcode = 0x75,
                Name = "ADC $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new AddWithCarry()
            };
            _instructions[0x76] = new Instruction
            {
                Opcode = 0x76,
                Name = "ROR $aa,X",
                RequiredClockCycles = 6,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Shift(false, true)
            };
            _instructions[0x78] = new Instruction
            {
                Opcode = 0x78,
                Name = "SEI",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.IRQDisable, true)
            };

        }

        public Instruction GetInstruction(byte opcode)
        {
            return _instructions[opcode];
        }
    }
}

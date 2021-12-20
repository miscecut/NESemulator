using NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations;
using NESEmulator.CPU.InstructionSet.Operations.ArithmeticOperations;
using NESEmulator.CPU.InstructionSet.Operations.BranchOperations;
using NESEmulator.CPU.InstructionSet.Operations.ControlOperations;
using NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations;
using NESEmulator.CPU.InstructionSet.Operations.LoadOperations;
using NESEmulator.CPU.InstructionSet.Operations.NopOperations;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;
using NESEmulator.CPU.InstructionSet.Operations.TransferOperations;
using NESEmulator.CPU.Registers;
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
            _instructions[0x79] = new Instruction
            {
                Opcode = 0x79,
                Name = "ADC $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new AddWithCarry()
            };
            _instructions[0x7D] = new Instruction
            {
                Opcode = 0x7D,
                Name = "ADC $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new AddWithCarry()
            };
            _instructions[0x7E] = new Instruction
            {
                Opcode = 0x7E,
                Name = "ROR $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Shift(false, true)
            };
            _instructions[0x81] = new Instruction
            {
                Opcode = 0x81,
                Name = "STA ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x84] = new Instruction
            {
                Opcode = 0x84,
                Name = "STY $aa,Y",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Store(Register.Y)
            };
            _instructions[0x85] = new Instruction
            {
                Opcode = 0x85,
                Name = "STA $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x86] = new Instruction
            {
                Opcode = 0x86,
                Name = "STX $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Store(Register.X)
            };
            _instructions[0x88] = new Instruction
            {
                Opcode = 0x88,
                Name = "DEY",
                RequiredClockCycles = 2,
                Operation = new ChangeRegisterByOne(Register.Y, false)
            };
            _instructions[0x8A] = new Instruction
            {
                Opcode = 0x8A,
                Name = "TXA",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.X, Register.Accumulator)
            };
            _instructions[0x8C] = new Instruction
            {
                Opcode = 0x8C,
                Name = "STY $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Store(Register.Y)
            };
            _instructions[0x8D] = new Instruction
            {
                Opcode = 0x8D,
                Name = "STA $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x8E] = new Instruction
            {
                Opcode = 0x8E,
                Name = "STX $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Store(Register.X)
            };
            _instructions[0x90] = new Instruction
            {
                Opcode = 0x90,
                Name = "BCC",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Carry, false)
            };
            _instructions[0x91] = new Instruction
            {
                Opcode = 0x91,
                Name = "STA ($aa),Y",
                RequiredClockCycles = 6,
                AddressingMode = new IndirectYIndexed(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x94] = new Instruction
            {
                Opcode = 0x94,
                Name = "STY $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Store(Register.Y)
            };
            _instructions[0x95] = new Instruction
            {
                Opcode = 0x95,
                Name = "STA $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x96] = new Instruction
            {
                Opcode = 0x96,
                Name = "STX $aa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageYIndexed(),
                Operation = new Store(Register.X)
            };
            _instructions[0x98] = new Instruction
            {
                Opcode = 0x98,
                Name = "TYA",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.Y, Register.Accumulator)
            };
            _instructions[0x99] = new Instruction
            {
                Opcode = 0x99,
                Name = "STA $aaaa,Y",
                RequiredClockCycles = 5,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0x9A] = new Instruction
            {
                Opcode = 0x9A,
                Name = "TXS",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.X, Register.StackPointer)
            };
            _instructions[0x9D] = new Instruction
            {
                Opcode = 0x9D,
                Name = "STA $aaaa,X",
                RequiredClockCycles = 5,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Store(Register.Accumulator)
            };
            _instructions[0xA0] = new Instruction
            {
                Opcode = 0xA0,
                Name = "LDY #$aa",
                RequiredClockCycles = 2,
                Operation = new Load(Register.Y)
            };
            _instructions[0xA1] = new Instruction
            {
                Opcode = 0xA1,
                Name = "LDA ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xA2] = new Instruction
            {
                Opcode = 0xA2,
                Name = "LDX #$aa",
                RequiredClockCycles = 2,
                Operation = new Load(Register.X)
            };
            _instructions[0xA4] = new Instruction
            {
                Opcode = 0xA4,
                Name = "LDY $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Load(Register.Y)
            };
            _instructions[0xA5] = new Instruction
            {
                Opcode = 0xA5,
                Name = "LDA $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xA6] = new Instruction
            {
                Opcode = 0xA6,
                Name = "LDX $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Load(Register.X)
            };
            _instructions[0xA8] = new Instruction
            {
                Opcode = 0xA8,
                Name = "TAY",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.Accumulator, Register.Y)
            };
            _instructions[0xA9] = new Instruction
            {
                Opcode = 0xA9,
                Name = "LDA #$aa",
                RequiredClockCycles = 2,
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xAA] = new Instruction
            {
                Opcode = 0xAA,
                Name = "TAX",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.Accumulator, Register.X)
            };
            _instructions[0xAC] = new Instruction
            {
                Opcode = 0xAC,
                Name = "LDY $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Load(Register.Y)
            };
            _instructions[0xAD] = new Instruction
            {
                Opcode = 0xAD,
                Name = "LDA $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xAE] = new Instruction
            {
                Opcode = 0xAE,
                Name = "LDX $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Load(Register.X)
            };
            _instructions[0xB0] = new Instruction
            {
                Opcode = 0xB0,
                Name = "BCS",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Carry, true)
            };
            _instructions[0xB1] = new Instruction
            {
                Opcode = 0xB1,
                Name = "LDY ($aa),Y",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new Load(Register.Y)
            };
            _instructions[0xB4] = new Instruction
            {
                Opcode = 0xB4,
                Name = "LDY $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Load(Register.Y)
            };
            _instructions[0xB5] = new Instruction
            {
                Opcode = 0xB5,
                Name = "LDA $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xB6] = new Instruction
            {
                Opcode = 0xB6,
                Name = "LDX $aa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageYIndexed(),
                Operation = new Load(Register.X)
            };
            _instructions[0xB8] = new Instruction
            {
                Opcode = 0xB8,
                Name = "CLV",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.Overflow, false)
            };
            _instructions[0xB9] = new Instruction
            {
                Opcode = 0xB9,
                Name = "LDA $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xBA] = new Instruction
            {
                Opcode = 0xBA,
                Name = "TSX",
                RequiredClockCycles = 2,
                Operation = new TransferBetweenRegisters(Register.StackPointer, Register.X)
            };
            _instructions[0xBC] = new Instruction
            {
                Opcode = 0xBC,
                Name = "LDY $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Load(Register.Y)
            };
            _instructions[0xBD] = new Instruction
            {
                Opcode = 0xBD,
                Name = "LDA $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Load(Register.Accumulator)
            };
            _instructions[0xBE] = new Instruction
            {
                Opcode = 0xBE,
                Name = "LDX $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Load(Register.X)
            };
            _instructions[0xC0] = new Instruction
            {
                Opcode = 0xC0,
                Name = "CPY #$aa",
                RequiredClockCycles = 2,
                Operation = new Compare(Register.Y)
            };
            _instructions[0xC1] = new Instruction
            {
                Opcode = 0xC1,
                Name = "CMP ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xC4] = new Instruction
            {
                Opcode = 0xC4,
                Name = "CPY $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Compare(Register.Y)
            };
            _instructions[0xC5] = new Instruction
            {
                Opcode = 0xC5,
                Name = "CMP $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xC6] = new Instruction
            {
                Opcode = 0xC6,
                Name = "DEC $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new ChangeMemoryByOne(false)
            };
            _instructions[0xC8] = new Instruction
            {
                Opcode = 0xC8,
                Name = "INY",
                RequiredClockCycles = 2,
                Operation = new ChangeRegisterByOne(Register.Y, true)
            };
            _instructions[0xC9] = new Instruction
            {
                Opcode = 0xC9,
                Name = "CMP #$aa",
                RequiredClockCycles = 2,
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xCA] = new Instruction
            {
                Opcode = 0xCA,
                Name = "DEX",
                RequiredClockCycles = 2,
                Operation = new ChangeRegisterByOne(Register.X, false)
            };
            _instructions[0xCC] = new Instruction
            {
                Opcode = 0xCC,
                Name = "CPY $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Compare(Register.Y)
            };
            _instructions[0xCD] = new Instruction
            {
                Opcode = 0xCD,
                Name = "CMP $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xCE] = new Instruction
            {
                Opcode = 0xCE,
                Name = "DEC $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new ChangeMemoryByOne(false)
            };
            _instructions[0xD0] = new Instruction
            {
                Opcode = 0xD0,
                Name = "BNE",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Zero, false)
            };
            _instructions[0xD1] = new Instruction
            {
                Opcode = 0xD1,
                Name = "CMP ($aa),Y",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xD5] = new Instruction
            {
                Opcode = 0xD5,
                Name = "CMP $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xD6] = new Instruction
            {
                Opcode = 0xD6,
                Name = "DEC $aa",
                RequiredClockCycles = 6,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new ChangeMemoryByOne(false)
            };
            _instructions[0xD8] = new Instruction
            {
                Opcode = 0xD8,
                Name = "CLD",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.DecimalMode, false)
            };
            _instructions[0xD9] = new Instruction
            {
                Opcode = 0xD9,
                Name = "CMP $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xDD] = new Instruction
            {
                Opcode = 0xDD,
                Name = "CMP $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new Compare(Register.Accumulator)
            };
            _instructions[0xDE] = new Instruction
            {
                Opcode = 0xDE,
                Name = "DEC $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new ChangeMemoryByOne(false)
            };
            _instructions[0xE0] = new Instruction
            {
                Opcode = 0xE0,
                Name = "CPX #$aa",
                RequiredClockCycles = 2,
                Operation = new Compare(Register.X)
            };
            _instructions[0xE1] = new Instruction
            {
                Opcode = 0xE1,
                Name = "SBC ($aa,X)",
                RequiredClockCycles = 6,
                AddressingMode = new IndexedXIndirect(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xE4] = new Instruction
            {
                Opcode = 0xE4,
                Name = "CPX $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new Compare(Register.X)
            };
            _instructions[0xE5] = new Instruction
            {
                Opcode = 0xE5,
                Name = "SBC $aa",
                RequiredClockCycles = 3,
                AddressingMode = new ZeroPage(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xE6] = new Instruction
            {
                Opcode = 0xE6,
                Name = "INC $aa",
                RequiredClockCycles = 5,
                AddressingMode = new ZeroPage(),
                Operation = new ChangeMemoryByOne(true)
            };
            _instructions[0xE8] = new Instruction
            {
                Opcode = 0xE8,
                Name = "INX",
                RequiredClockCycles = 2,
                Operation = new ChangeRegisterByOne(Register.X, true)
            };
            _instructions[0xE9] = new Instruction
            {
                Opcode = 0xE9,
                Name = "SBC #$aa",
                RequiredClockCycles = 2,
                Operation = new SubtractWithCarry()
            };
            _instructions[0xEA] = new Instruction
            {
                Opcode = 0xEA,
                Name = "NOP",
                RequiredClockCycles = 2,
                Operation = new Nop()
            };
            _instructions[0xEC] = new Instruction
            {
                Opcode = 0xEC,
                Name = "CPX $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new Compare(Register.X)
            };
            _instructions[0xED] = new Instruction
            {
                Opcode = 0xED,
                Name = "SBC $aaaa",
                RequiredClockCycles = 4,
                AddressingMode = new Absolute(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xEE] = new Instruction
            {
                Opcode = 0xEE,
                Name = "INC $aaaa",
                RequiredClockCycles = 2,
                AddressingMode = new Absolute(),
                Operation = new ChangeMemoryByOne(true)
            };
            _instructions[0xEE] = new Instruction
            {
                Opcode = 0xEE,
                Name = "INC $aaaa",
                RequiredClockCycles = 6,
                AddressingMode = new Absolute(),
                Operation = new ChangeMemoryByOne(true)
            };
            _instructions[0xF0] = new Instruction
            {
                Opcode = 0xF0,
                Name = "BEQ",
                RequiredClockCycles = 2,
                AddressingMode = new Relative(),
                Operation = new Branch(StatusRegisterFlags.Zero, true)
            };
            _instructions[0xF1] = new Instruction
            {
                Opcode = 0xF1,
                Name = "SBC ($aa),Y",
                RequiredClockCycles = 5,
                AddressingMode = new IndirectYIndexed(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xF5] = new Instruction
            {
                Opcode = 0xF5,
                Name = "SBC $aa,X",
                RequiredClockCycles = 4,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xF6] = new Instruction
            {
                Opcode = 0xF6,
                Name = "INC $aa,X",
                RequiredClockCycles = 6,
                AddressingMode = new ZeroPageXIndexed(),
                Operation = new ChangeMemoryByOne(true)
            };
            _instructions[0xF8] = new Instruction
            {
                Opcode = 0xF8,
                Name = "SED",
                RequiredClockCycles = 2,
                Operation = new ChangeFlag(StatusRegisterFlags.DecimalMode, true)
            };
            _instructions[0xF9] = new Instruction
            {
                Opcode = 0xF9,
                Name = "SBC $aaaa,Y",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteYIndexed(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xFD] = new Instruction
            {
                Opcode = 0xFD,
                Name = "SBC $aaaa,X",
                RequiredClockCycles = 4,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new SubtractWithCarry()
            };
            _instructions[0xFE] = new Instruction
            {
                Opcode = 0xF6,
                Name = "INC $aaaa,X",
                RequiredClockCycles = 7,
                AddressingMode = new AbsoluteXIndexed(),
                Operation = new ChangeMemoryByOne(true)
            };
        }

        public Instruction GetInstruction(byte opcode)
        {
            return _instructions[opcode];
        }
    }
}

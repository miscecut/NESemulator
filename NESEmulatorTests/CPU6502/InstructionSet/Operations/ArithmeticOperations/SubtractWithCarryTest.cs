using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.ArithmeticOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.ArithmeticOperations
{
    [TestClass]
    public class SubtractWithCarryTest
    {
        [TestMethod]
        public void TestSubtractSimple()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, true);
            registers.SetRegister(Register.Accumulator, 0b00110110); //54
            registers.SetProgramCounter(0x19FF);
            bus.CPUWrite(0x19FF, 0b00100000); //32

            new SubtractWithCarry().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1A00);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b00010110); //54 - 32 = 22
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestSubtractSimpleWithCarryIn()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, false);
            registers.SetRegister(Register.Accumulator, 0b01011000); //88
            registers.SetProgramCounter(0x19FF);
            bus.CPUWrite(0x19FF, 0b00000010); //2

            new SubtractWithCarry().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1A00);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b01010101); //88 - 2 - 1 = 85
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestSubtractUnderZero()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, true);
            registers.SetRegister(Register.Accumulator, 0b00000001); //1
            registers.SetProgramCounter(0x19FF);
            bus.CPUWrite(0x19FF, 0b00111100); //60

            new SubtractWithCarry().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1A00);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b11000101); //1 - 60 = -59
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestSubtractResultIsZeroWithCarry()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, false);
            registers.SetRegister(Register.Accumulator, 0b00001000); //8
            bus.CPUWrite(0x19FF, 0b00000111); //7

            new SubtractWithCarry().OperationWithAddress(bus, registers, 0x19FF);

            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b00000000); //8 - 7 - 1 = 0
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

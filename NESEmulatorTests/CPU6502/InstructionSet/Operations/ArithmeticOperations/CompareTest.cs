using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.ArithmeticOperations
{
    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        public void TestComparePositiveDifference()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.Accumulator, 0x05);
            registers.SetProgramCounter(0xDD91);
            bus.CPUWrite(0xDD91, 0x03);

            new Compare(Register.Accumulator).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0xDD92);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0x05); //the accumulator did not change
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestCompareNegativeDifference()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.X, 0xAA);
            registers.SetProgramCounter(0xDD91);
            bus.CPUWrite(0xDD91, 0xBC);

            new Compare(Register.X).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0xDD92);
            Assert.AreEqual(registers.GetRegister(Register.X), 0xAA); //the x register did not change
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestCompareZeroDifference()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.Y, 0x67);
            registers.SetProgramCounter(0x5A42);
            bus.CPUWrite(0xDD91, 0x67);

            new Compare(Register.Y).OperationWithAddress(bus, registers, 0xDD91);

            Assert.AreEqual(registers.GetProgramCounter(), 0x5A42);
            Assert.AreEqual(registers.GetRegister(Register.Y), 0x67); //the y register did not change
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

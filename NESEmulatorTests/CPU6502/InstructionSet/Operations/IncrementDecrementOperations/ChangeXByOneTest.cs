using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class ChangeXByOneTest
    {
        [TestMethod]
        public void TestIncrementX()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.X = 0b11110000;

            new ChangeXByOne(true).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.X, 0b11110001);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestIncrementXOverflow()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.X = 0b11111111;

            new ChangeXByOne(true).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.X, 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
        }

        [TestMethod]
        public void TestDecrementX()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.X = 0b11111111;

            new ChangeXByOne(false).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.X, 0b11111110);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestDecrementXOverflow()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.X = 0b00000000;

            new ChangeXByOne(false).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.X, 0b11111111);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.IncrementDecrementOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class ChangeMemoryByOneTest
    {
        [TestMethod]
        public void TestIncrementMemory()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            bus.CPUWrite(0x019B, 0b10111110);

            new ChangeMemoryByOne(true).OperationWithAddress(bus, registers, 0x019B);

            Assert.AreEqual(bus.CPURead(0x019B), 0b10111111);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestIncrementMemoryOverflow()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            bus.CPUWrite(0xFFF3, 0b11111111);

            new ChangeMemoryByOne(true).OperationWithAddress(bus, registers, 0xFFF3);

            Assert.AreEqual(bus.CPURead(0xFFF3), 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestDecrementMemory()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            bus.CPUWrite(0x019B, 0b00111010);

            new ChangeMemoryByOne(false).OperationWithAddress(bus, registers, 0x019B);

            Assert.AreEqual(bus.CPURead(0x019B), 0b00111001);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestDecrementMemoryOverflow()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            bus.CPUWrite(0xFFF3, 0b00000000);

            new ChangeMemoryByOne(false).OperationWithAddress(bus, registers, 0xFFF3);

            Assert.AreEqual(bus.CPURead(0xFFF3), 0b11111111);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

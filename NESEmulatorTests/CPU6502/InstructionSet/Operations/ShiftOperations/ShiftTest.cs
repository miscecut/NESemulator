using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.ShiftOperations
{
    [TestClass]
    public class ShiftTest
    {
        [TestMethod]
        public void TestShiftLeftNoRotateAccumulator()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.A, 0b11011100);
            registers.SetFlag(StatusRegisterFlags.Carry, true);

            new Shift(true, false).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetRegister(Register.A), 0b10111000);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
        }
        [TestMethod]
        public void TestShiftLeftRotateAccumulator()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.A, 0b01101110);
            registers.SetFlag(StatusRegisterFlags.Carry, true);

            new Shift(true, true).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetRegister(Register.A), 0b11011101);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
        }

        [TestMethod]
        public void TestShiftRightNoRotateMemory()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            bus.CPUWrite(0x0950, 0b00001101);
            registers.SetFlag(StatusRegisterFlags.Carry, true);

            new Shift(false, false).OperationWithAddress(bus, registers, 0x0950);

            Assert.AreEqual(bus.CPURead(0x0950), 0b00000110);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
        }
        [TestMethod]
        public void TestShiftRightRotateMemory()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            bus.CPUWrite(0x0950, 0b10000000);
            registers.SetFlag(StatusRegisterFlags.Carry, true);

            new Shift(false, true).OperationWithAddress(bus, registers, 0x0950);

            Assert.AreEqual(bus.CPURead(0x0950), 0b11000000);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
        }

    }
}

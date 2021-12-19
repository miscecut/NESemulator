using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class AddWithCarryTest
    {
        [TestMethod]
        public void TestADCImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, true);
            registers.A = 0b01010001;
            registers.ProgramCounter = 0x019B;
            bus.CPUWrite(0x019B, 0b00101101);

            new AddWithCarry().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x019C);
            Assert.AreEqual(registers.A, 0b01111111);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestADCImmediateWithOverflow()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, false);
            registers.A = 0b10000010;
            registers.ProgramCounter = 0x0450;
            bus.CPUWrite(0x0450, 0b11000010);

            new AddWithCarry().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x0451);
            Assert.AreEqual(registers.A, 0b01000100);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestADCWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, false);
            registers.A = 0b11111111;
            registers.ProgramCounter = 0xAAAA;
            ushort addendAddress = 0xB400;
            bus.CPUWrite(0xB400, 0b00000001);

            new AddWithCarry().OperationWithAddress(bus, registers, addendAddress);

            Assert.AreEqual(registers.ProgramCounter, 0xAAAA);
            Assert.AreEqual(registers.A, 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

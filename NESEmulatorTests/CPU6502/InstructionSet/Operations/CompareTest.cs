using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class CompareTest
    {
        [TestMethod]
        public void TestCompareImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            registers.A = 0b10010001;
            registers.ProgramCounter = 0x019B;
            bus.CPUWrite(0x019B, 0b10010001);

            new Compare().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x019C);
            Assert.AreEqual(registers.A, 0b10010001);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestCompareWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.A = 0b11001010;
            ushort operatorAddress = 0xB400;
            bus.CPUWrite(0xB400, 0b11101010);

            new Compare().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.A, 0b11001010);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestCompareXImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            registers.X = 0b11111111;
            registers.ProgramCounter = 0x019B;
            bus.CPUWrite(0x019B, 0b11111111);

            new CompareX().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x019C);
            Assert.AreEqual(registers.X, 0b11111111);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

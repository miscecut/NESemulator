using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class XorTest
    {
        [TestMethod]
        public void TestXorImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            registers.A = 0b00011100;
            registers.ProgramCounter = 0x019B;
            bus.CPUWrite(0x019B, 0b01010101);

            new Xor().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x019C);
            Assert.AreEqual(registers.A, 0b01001001);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestXorWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.A = 0b10101010;
            registers.ProgramCounter = 0x1101;
            ushort operatorAddress = 0xACDC;
            bus.CPUWrite(0xACDC, 0b10101010);

            new Xor().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.ProgramCounter, 0x1101);
            Assert.AreEqual(registers.A, 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

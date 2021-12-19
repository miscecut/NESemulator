using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class BitTest
    {
        [TestMethod]
        public void TestBitImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.A = 0b01011100;
            registers.ProgramCounter = 0x019B;
            bus.CPUWrite(0x019B, 0b01010101);
            //BIT result is: 0101 0100

            new Bit().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.ProgramCounter, 0x019C);
            Assert.AreEqual(registers.A, 0b01011100); //the accumulator did not change
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Overflow));
        }

        [TestMethod]
        public void TestBitWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.A = 0b10000000;
            registers.ProgramCounter = 0x1101;
            ushort operatorAddress = 0xACDC;
            bus.CPUWrite(0xACDC, 0b10111111);
            //BIT result is: 1000 00000

            new Bit().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.ProgramCounter, 0x1101);
            Assert.AreEqual(registers.A, 0b10000000);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
        }
    }
}

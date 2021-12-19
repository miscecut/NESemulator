using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.Registers;

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
            registers.SetRegister(Register.A, 0b00011100);
            registers.SetProgramCounter(0x019B);
            bus.CPUWrite(0x019B, 0b01010101);

            new Xor().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x019C);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b01001001);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestXorWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.A, 0b10101010);
            registers.SetProgramCounter(0x1101);
            ushort operatorAddress = 0xACDC;
            bus.CPUWrite(0xACDC, 0b10101010);

            new Xor().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1101);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

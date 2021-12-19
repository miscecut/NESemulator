using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class OrTest
    {
        [TestMethod]
        public void TestOrImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            registers.SetRegister(Register.A, 0b11110000);
            registers.SetProgramCounter(0x019B);
            bus.CPUWrite(0x019B, 0b11001100);

            new Or().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x019C);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b11111100);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestOrWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.A, 0b01110111);
            registers.SetProgramCounter(0x1101);
            ushort operatorAddress = 0xB400;
            bus.CPUWrite(0xB400, 0b11111111);

            new Or().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1101);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b11111111);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

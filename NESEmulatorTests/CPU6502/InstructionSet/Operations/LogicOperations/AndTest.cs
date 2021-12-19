using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.OperationImplementation;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations
{
    [TestClass]
    public class AndTest
    {
        [TestMethod]
        public void TestANDImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Negative, true);
            registers.SetRegister(Register.A, 0b10010001);
            registers.SetProgramCounter(0x019B);
            bus.CPUWrite(0x019B, 0b11011101);

            new And().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x019C);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b10010001);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestANDWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.A, 0b01110111);
            registers.SetProgramCounter(0x1101);
            ushort operatorAddress = 0xB400;
            bus.CPUWrite(0xB400, 0b00000000);

            new And().OperationWithAddress(bus, registers, operatorAddress);

            Assert.AreEqual(registers.GetProgramCounter(), 0x1101);
            Assert.AreEqual(registers.GetRegister(Register.A), 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

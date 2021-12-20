using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.TransferOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.TransferOperations
{
    [TestClass]
    public class TransferBetweenRegistersTest
    {
        [TestMethod]
        public void TestTransferBetweenRegistersYtoA()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.Accumulator, 0b00001000);
            registers.SetRegister(Register.Y, 0b10110000);
            registers.SetProgramCounter(0xB5A6);

            new TransferBetweenRegisters(Register.Y, Register.Accumulator).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0xB5A6);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b10110000);
            Assert.AreEqual(registers.GetRegister(Register.Y), 0b10110000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
        }

        [TestMethod]
        public void TestTransferBetweenRegistersSPtoX()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.StackPointer, 0x54);
            registers.SetRegister(Register.X, 0xBB);
            registers.SetProgramCounter(0xB5A6);

            new TransferBetweenRegisters(Register.StackPointer, Register.X).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0xB5A6);
            Assert.AreEqual(registers.GetRegister(Register.StackPointer), 0x54);
            Assert.AreEqual(registers.GetRegister(Register.X), 0x54);
        }
    }
}

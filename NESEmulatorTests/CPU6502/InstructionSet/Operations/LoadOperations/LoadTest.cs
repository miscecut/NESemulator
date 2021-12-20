using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.LoadOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.LoadOperations
{
    [TestClass]
    public class LoadTest
    {
        [TestMethod]
        public void TestLoadImmediate()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetProgramCounter(0x0404);
            bus.CPUWrite(0x0404, 0b01010110);

            new Load(Register.X).OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x0405);
            Assert.AreEqual(registers.GetRegister(Register.X), 0b01010110);
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }

        [TestMethod]
        public void TestLoadWithAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.X, 0xF3);
            registers.SetProgramCounter(0x0404);
            bus.CPUWrite(0x0404, 0b01010110);
            bus.CPUWrite(0x8890, 0b00000000);

            new Load(Register.Accumulator).OperationWithAddress(bus, registers, 0x8890);

            Assert.AreEqual(registers.GetProgramCounter(), 0x0404);
            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0b00000000);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Zero));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Negative));
        }
    }
}

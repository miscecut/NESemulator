using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.ControlOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.ControlOperations
{
    [TestClass]
    public class JumpTest
    {
        [TestMethod]
        public void TestJump()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetProgramCounter(0x019B);
            registers.SetStackPointer(0x03);
            bus.CPUWrite(0x0102, 0x33);
            bus.CPUWrite(0x0101, 0x22);

            new Jump(false).OperationWithAddress(bus, registers, 0x950B);

            Assert.AreEqual(registers.GetProgramCounter(), 0x950B);
            Assert.AreEqual(registers.GetStackPointer(), 0x03); //the stack did not change
            Assert.AreEqual(bus.CPURead(0x0102), 0x33); //the stack was not overridden
            Assert.AreEqual(bus.CPURead(0x0101), 0x22); //the stack was not overridden
        }

        [TestMethod]
        public void TestJumpWithProgramCounterPushOnStack()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetProgramCounter(0x019B);
            registers.SetStackPointer(0x03);
            bus.CPUWrite(0x0103, 0x33);
            bus.CPUWrite(0x0102, 0x22);

            new Jump(true).OperationWithAddress(bus, registers, 0x950B);

            Assert.AreEqual(registers.GetProgramCounter(), 0x950B);
            Assert.AreEqual(registers.GetStackPointer(), 0x01); //the stack changed in order to push the program counter
            Assert.AreEqual(bus.CPURead(0x0103), 0x01); //hi old program counter
            Assert.AreEqual(bus.CPURead(0x0102), 0x9A); //lo old program counter
        }
    }
}

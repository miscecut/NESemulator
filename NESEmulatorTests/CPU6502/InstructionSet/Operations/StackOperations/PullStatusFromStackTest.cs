using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.StackOperations
{
    [TestClass]
    public class PullStatusFromStackTest
    {
        [TestMethod]
        public void TestPullStatusFromStack()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetStatus(0b00110011);
            registers.SetRegister(Register.StackPointer, 0x06);
            bus.CPUWrite(0x0107, 0b11011101);

            new PullStatusFromStack().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetRegister(Register.StackPointer), 0x07);
            Assert.AreEqual(registers.GetStatus(), 0b11111101);
        }
    }
}

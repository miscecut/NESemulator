using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;

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

            registers.Status = 0b00110011;
            registers.StackPointer = 0x06;
            bus.CPUWrite(0x0107, 0b11011101);

            new PullStatusFromStack().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.StackPointer, 0x07);
            Assert.AreEqual(registers.Status, 0b11111101);
        }
    }
}

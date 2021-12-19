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

            registers.SetStatus(0b00110011);
            registers.SetStackPointer(0x06);
            bus.CPUWrite(0x0107, 0b11011101);

            new PullStatusFromStack().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetStackPointer(), 0x07);
            Assert.AreEqual(registers.GetStatus(), 0b11111101);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.StackOperations
{
    [TestClass]
    public class PullAccumulatorFromStackTest
    {
        [TestMethod]
        public void TestPullAccumulatorFromStack()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.StackPointer = 0x59;
            bus.CPUWrite(0x015A, 0xDE);

            new PullAccumulatorFromStack().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.StackPointer, 0x5A);
            Assert.AreEqual(registers.A, 0xDE);
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Negative));
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Zero));
        }
    }
}

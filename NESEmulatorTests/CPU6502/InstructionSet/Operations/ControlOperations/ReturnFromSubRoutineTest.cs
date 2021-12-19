using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.ControlOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.ControlOperations
{
    [TestClass]
    public class ReturnFromSubRoutineTest
    {
        [TestMethod]
        public void TestReturnFromSubRoutine()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetProgramCounter(0x019B);
            registers.SetStackPointer(0x05);
            bus.CPUWrite(0x0106, 0x33);
            bus.CPUWrite(0x0107, 0x22);

            new ReturnFromSubRoutine().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetProgramCounter(), 0x2234);
            Assert.AreEqual(registers.GetStackPointer(), 0x07);
        }
    }
}

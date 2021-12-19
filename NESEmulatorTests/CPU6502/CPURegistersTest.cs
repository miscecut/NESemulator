using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.CPU;

namespace NESEmulatorTests.CPU6502
{
    [TestClass]
    public class CPURegisterTest
    {
        [TestMethod]
        public void TestProgramCounter()
        {
            var registers = new CPURegisters();
            var initialProgramCounter = registers.ProgramCounter;
            registers.IncrementProgramCounter();
            var newProgramCounter = registers.ProgramCounter;
            Assert.IsTrue(newProgramCounter - initialProgramCounter == 0x0001);
            newProgramCounter = registers.GetProgramCounterAndIncrement();
            Assert.IsTrue(newProgramCounter - initialProgramCounter == 0x0001);
            newProgramCounter = registers.ProgramCounter;
            Assert.IsTrue(newProgramCounter - initialProgramCounter == 0x0002);
        }
    }
}

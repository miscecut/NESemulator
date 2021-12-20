using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.LoadOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.LoadOperations
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void TestStore()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.Y, 0xF3);
            bus.CPUWrite(0xE4AF, 0x40);

            new Store(Register.Y).OperationWithAddress(bus, registers, 0xE4AF);

            Assert.AreEqual(registers.GetRegister(Register.Y), 0xF3);
            Assert.AreEqual(bus.CPURead(0xE4AF), 0xF3);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.CPU;

namespace NESEmulatorTests.CPU6502
{
    [TestClass]
    public class CPURegisterFlagsTest
    {
        [TestMethod]
        public void TestSetFlags()
        {
            var registers = new CPURegisters();
            Assert.AreEqual(registers.Status, 0b00100000); //correct status init

            registers.SetFlag(StatusRegisterFlags.Carry, true);
            Assert.AreEqual(registers.Status, 0b00100001); //last significant bit is set;
            registers.SetFlag(StatusRegisterFlags.Overflow, true);
            Assert.AreEqual(registers.Status, 0b01100001); //sixth bit is set;
            registers.SetFlag(StatusRegisterFlags.Overflow, true);
            Assert.AreEqual(registers.Status, 0b01100001); //sixth bit is not changed
        }

        [TestMethod]
        public void TestUnsetFlags()
        {
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, true);
            Assert.AreEqual(registers.Status, 0b00100001); //last significant bit is set;
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Carry));
            registers.SetFlag(StatusRegisterFlags.Overflow, true);
            Assert.AreEqual(registers.Status, 0b01100001); //sixth bit is set;
            Assert.IsTrue(registers.GetFlag(StatusRegisterFlags.Overflow));
            registers.SetFlag(StatusRegisterFlags.Carry, false);
            Assert.AreEqual(registers.Status, 0b01100000); //Carry bit is unset
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Carry));
            registers.SetFlag(StatusRegisterFlags.Overflow, false);
            Assert.AreEqual(registers.Status, 0b00100000); //Carry bit is still unset
            Assert.IsFalse(registers.GetFlag(StatusRegisterFlags.Overflow));
        }
    }
}

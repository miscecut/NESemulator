using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.BranchOperations;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.BranchOperations
{
    [TestClass]
    public class BranchTest
    {
        [TestMethod]
        public void TestBranchNotTaken()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Carry, false);
            registers.ProgramCounter = 0x1163;

            var branchResult = new Branch(StatusRegisterFlags.Carry, true).OperationWithAddress(bus, registers, 0xFF9A);

            Assert.AreEqual(registers.ProgramCounter, 0x1163); //the program counter did not change because the branch was not taken
            Assert.AreEqual(branchResult, 0); //no additional cycles for branches not taken
        }

        [TestMethod]
        public void TestBranchTakenPageNotCrossed()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Overflow, false);
            registers.ProgramCounter = 0x1163;

            var branchResult = new Branch(StatusRegisterFlags.Overflow, false).OperationWithAddress(bus, registers, 0x0019);

            Assert.AreEqual(registers.ProgramCounter, 0x117C);
            Assert.AreEqual(branchResult, 1);
        }

        [TestMethod]
        public void TestBranchTakenPageCrossed()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Overflow, false);
            registers.ProgramCounter = 0xFAFA;

            var branchResult = new Branch(StatusRegisterFlags.Overflow, false).OperationWithAddress(bus, registers, 0x0009);

            Assert.AreEqual(registers.ProgramCounter, 0xFB03);
            Assert.AreEqual(branchResult, 2); //branch taken and page crossed
        }

        [TestMethod]
        public void TestBranchTakenPageCrossedNegativeRelativeAddress()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetFlag(StatusRegisterFlags.Overflow, false);
            registers.ProgramCounter = 0x0501;

            var branchResult = new Branch(StatusRegisterFlags.Overflow, false).OperationWithAddress(bus, registers, 0b1111111111111110); //-2

            Assert.AreEqual(registers.ProgramCounter, 0x04FF);
            Assert.AreEqual(branchResult, 2); //branch taken and page crossed
        }
    }
}

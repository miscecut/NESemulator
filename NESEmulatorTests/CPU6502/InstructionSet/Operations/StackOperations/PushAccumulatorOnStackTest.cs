using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.Operations.StackOperations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.Operations.StackOperations
{
     [TestClass]
    public class PushAccumulatorOnStackTest
    {
        [TestMethod]
        public void TestPushAccumulator()
        {
            var bus = new BusWithOnlyRAM();
            var registers = new CPURegisters();

            registers.SetRegister(Register.Accumulator, 0x56);
            registers.SetRegister(Register.StackPointer, 0xAA);

            new PushAccumulatorOnStack().OperationImmediate(bus, registers);

            Assert.AreEqual(registers.GetRegister(Register.Accumulator), 0x56); //the accumulator did not change
            Assert.AreEqual(registers.GetRegister(Register.StackPointer), 0xA9); //the stack pointer was reduced by 1
            Assert.AreEqual(bus.CPURead(0x01AA), 0x56);
        }
    }
}

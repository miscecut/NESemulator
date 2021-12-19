using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Bus;
using NESEmulator.CPU;
using NESEmulator.CPU.InstructionSet.AddressingModes.AddressingModeImplementations;
using NESEmulator.CPU.Registers;

namespace NESEmulatorTests.CPU6502.InstructionSet.AddressingModes
{
    [TestClass]
    public class AddressingModesTest
    {
        private IBus _bus;
        private readonly CPURegisters _registers = new CPURegisters();

        [TestMethod]
        public void TestAbsolute()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absolute = new Absolute();
            _registers.SetProgramCounter(0x1122);
            _bus.CPUWrite(0x1122, 0xAB); //lo absolute address
            _bus.CPUWrite(0x1123, 0x4F); //hi absolute address
            var absoluteResult = absolute.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteResult.Address, 0x4FAB);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x1124); //2 ticks of program counter (for the address parts) are requested
        }

        [TestMethod]
        public void TestZeroPage()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var zeroPage = new ZeroPage();
            _registers.SetProgramCounter(0x1122);
            _bus.CPUWrite(0x1122, 0x34); //lo absolute address
            var zeroPageResult = zeroPage.Fetch(_bus, _registers);
            Assert.AreEqual(zeroPageResult.Address, 0x0034);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x1123); //1 tick of program counter (for the lo address part) is requested
        }

        [TestMethod]
        public void TestAbsoluteXIndexed()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteX = new AbsoluteXIndexed();
            _registers.SetProgramCounter(0x0A32);
            _registers.SetRegister(Register.X, 0x03);
            _bus.CPUWrite(0x0A32, 0x90); //lo absolute address
            _bus.CPUWrite(0x0A33, 0x02); //hi absolute address
            var absoluteXResult = absoluteX.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteXResult.Address, 0x0293);
            Assert.AreEqual(absoluteXResult.AdditionalCycles, 0); //I didn't cross the page 02
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0A34);
        }

        [TestMethod]
        public void TestAbsoluteXIndexedWithPageJump()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteX = new AbsoluteXIndexed();
            _registers.SetProgramCounter(0x0A32);
            _registers.SetRegister(Register.X, 0x02);
            _bus.CPUWrite(0x0A32, 0xFF); //lo absolute address
            _bus.CPUWrite(0x0A33, 0xB1); //hi absolute address
            var absoluteXResult = absoluteX.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteXResult.Address, 0xB201);
            Assert.AreEqual(absoluteXResult.AdditionalCycles, 1); //I crossed the page B1 to B2
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0A34);
        }

        [TestMethod]
        public void TestAbsoluteYIndexed()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteY = new AbsoluteYIndexed();
            _registers.SetProgramCounter(0x0A32);
            _registers.SetRegister(Register.Y, 0x03);
            _bus.CPUWrite(0x0A32, 0x90); //lo absolute address
            _bus.CPUWrite(0x0A33, 0x02); //hi absolute address
            var absoluteYResult = absoluteY.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteYResult.Address, 0x0293);
            Assert.AreEqual(absoluteYResult.AdditionalCycles, 0); //I didn't cross the page 02
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0A34);
        }

        [TestMethod]
        public void TestAbsoluteYIndexedWithPageJump()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteY = new AbsoluteYIndexed();
            _registers.SetProgramCounter(0x0A32);
            _registers.SetRegister(Register.Y, 0x02);
            _bus.CPUWrite(0x0A32, 0xFF); //lo absolute address
            _bus.CPUWrite(0x0A33, 0xB1); //hi absolute address
            var absoluteYResult = absoluteY.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteYResult.Address, 0xB201);
            Assert.AreEqual(absoluteYResult.AdditionalCycles, 1); //I crossed the page B1 to B2
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0A34);
        }

        [TestMethod]
        public void TestZeroPageXIndexed()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var zeroPageX = new ZeroPageXIndexed();
            _registers.SetProgramCounter(0x1256);
            _registers.SetRegister(Register.X, 0x08);
            _bus.CPUWrite(0x1256, 0x77); //lo absolute address
            var zeroPageXResult = zeroPageX.Fetch(_bus, _registers);
            Assert.AreEqual(zeroPageXResult.Address, 0x007F);
            Assert.AreEqual(zeroPageXResult.AdditionalCycles, 0); //I didn't cross the zero page
            Assert.AreEqual(_registers.GetProgramCounter(), 0x1257);
        }

        [TestMethod]
        public void TestZeroPageXIndexedTryingToJumpZeroPage()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var zeroPageX = new ZeroPageXIndexed();
            _registers.SetProgramCounter(0x1256);
            _registers.SetRegister(Register.X, 0x04);
            _bus.CPUWrite(0x1256, 0xFE); //lo absolute address
            var zeroPageXResult = zeroPageX.Fetch(_bus, _registers);
            Assert.AreEqual(zeroPageXResult.Address, 0x0002);
            Assert.AreEqual(zeroPageXResult.AdditionalCycles, 0); //It's impossibile to have a new required cycle
            Assert.AreEqual(_registers.GetProgramCounter(), 0x1257);
        }

        [TestMethod]
        public void TestZeroPageYIndexed()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var zeroPageY = new ZeroPageYIndexed();
            _registers.SetProgramCounter(0xF555);
            _registers.SetRegister(Register.Y, 0xA0);
            _bus.CPUWrite(0xF555, 0x1F); //lo absolute address
            var zeroPageYResult = zeroPageY.Fetch(_bus, _registers);
            Assert.AreEqual(zeroPageYResult.Address, 0x00BF);
            Assert.AreEqual(zeroPageYResult.AdditionalCycles, 0); //I didn't cross the zero page
            Assert.AreEqual(_registers.GetProgramCounter(), 0xF556);
        }

        [TestMethod]
        public void TestZeroPageYIndexedTryingToJumpZeroPage()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var zeroPageY = new ZeroPageYIndexed();
            _registers.SetProgramCounter(0x1256);
            _registers.SetRegister(Register.Y, 0x04);
            _bus.CPUWrite(0x1256, 0xFE); //lo absolute address
            var zeroPageYResult = zeroPageY.Fetch(_bus, _registers);
            Assert.AreEqual(zeroPageYResult.Address, 0x0002);
            Assert.AreEqual(zeroPageYResult.AdditionalCycles, 0); //It's impossibile to have a new required cycle
            Assert.AreEqual(_registers.GetProgramCounter(), 0x1257);
        }

        [TestMethod]
        public void TestAbsoluteIndirect()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new AbsoluteIndirect();
            _registers.SetProgramCounter(0x58A4);
            _bus.CPUWrite(0x58A4, 0x01); //lo absolute pointer address
            _bus.CPUWrite(0x58A5, 0x79); //hi absolute pointer address
            _bus.CPUWrite(0x7901, 0x65); //lo pointer
            _bus.CPUWrite(0x7902, 0x90); //hi pointer
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0x9065);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x58A6); //2 ticks of program counter (for the address parts) are requested
        }

        [TestMethod]
        public void TestAbsoluteIndirectHardwareBug()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new AbsoluteIndirect();
            _registers.SetProgramCounter(0x58A4);
            _bus.CPUWrite(0x58A4, 0xFF); //lo absolute pointer address
            _bus.CPUWrite(0x58A5, 0x79); //hi absolute pointer address
            _bus.CPUWrite(0x79FF, 0x65); //lo pointer
            _bus.CPUWrite(0x7900, 0x90); //hi pointer, this is the result of the bug: 7900 instead of 7A00 (page is not jumped)
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0x9065);
            Assert.AreNotEqual(absoluteIndirectResult.Address, 0x00);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x58A6); //2 ticks of program counter (for the address parts) are requested
        }

        [TestMethod]
        public void TestIndexedXIndirect()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new IndexedXIndirect();
            _registers.SetProgramCounter(0x0200);
            _registers.SetRegister(Register.X, 0x0003);
            _bus.CPUWrite(0x0200, 0xFC); //lo pointer address
            _bus.CPUWrite(0x00FF, 0x6E); //lo pointer
            _bus.CPUWrite(0x0000, 0x20); //hi pointer
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0x206E);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0201);
        }

        [TestMethod]
        public void TestIndirectYIndexed()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new IndirectYIndexed();
            _registers.SetProgramCounter(0x0200);
            _registers.SetRegister(Register.Y, 0x0002);
            _bus.CPUWrite(0x0200, 0xFC); //lo pointer lo address
            _bus.CPUWrite(0x00FC, 0x6E); //lo pointer
            _bus.CPUWrite(0x00FD, 0x20); //hi pointer
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0x2070);
            Assert.AreEqual(absoluteIndirectResult.AdditionalCycles, 0);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x0201);
        }

        [TestMethod]
        public void TestIndirectYIndexedWithPageJump()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new IndirectYIndexed();
            _registers.SetProgramCounter(0x4441);
            _registers.SetRegister(Register.Y, 0x0001);
            _bus.CPUWrite(0x4441, 0xB3); //lo pointer lo address
            _bus.CPUWrite(0x00B3, 0xFF); //lo pointer
            _bus.CPUWrite(0x00B4, 0x66); //hi pointer
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0x6700);
            Assert.AreEqual(absoluteIndirectResult.AdditionalCycles, 1);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x4442);
        }

        [TestMethod]
        public void TestRelativePositiveLoAddress()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new Relative();
            _registers.SetProgramCounter(0x4441);
            _bus.CPUWrite(0x4441, 0b00011100); //lo pointer address
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0b0000000000011100);
            Assert.AreEqual(absoluteIndirectResult.AdditionalCycles, 0);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x4442);
        }

        [TestMethod]
        public void TestRelativeNegativeLoAddress()
        {
            _bus = new BusWithOnlyRAM();
            _registers.Reset();

            var absoluteIndirect = new Relative();
            _registers.SetProgramCounter(0x4441);
            _bus.CPUWrite(0x4441, 0b10100001); //lo pointer address
            var absoluteIndirectResult = absoluteIndirect.Fetch(_bus, _registers);
            Assert.AreEqual(absoluteIndirectResult.Address, 0b1111111110100001);
            Assert.AreEqual(absoluteIndirectResult.AdditionalCycles, 0);
            Assert.AreEqual(_registers.GetProgramCounter(), 0x4442);
        }
    }
}

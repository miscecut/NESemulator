using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.RAM;

namespace NESEmulatorTests.RAM
{
    [TestClass]
    public class MorroringRAMTest
    {
        [TestMethod]
        public void MirroringRAMTest()
        {
            var ram = new MirroringRAM();

            ram.Write(0x0000, 0xBB);

            Assert.AreEqual(ram.Read(0x000A), 0x00); //correct ram init
            Assert.AreEqual(ram.Read(0x0000), 0xBB); //correct write
            Assert.AreEqual(ram.Read(0x0800), 0xBB); //correct mirrored write
            Assert.AreEqual(ram.Read(0x1000), 0xBB); //correct mirrored write
            Assert.AreEqual(ram.Read(0x1800), 0xBB); //correct mirrored write

            ram.Write(0x00C0, 0x45);

            Assert.AreEqual(ram.Read(0x00C0), 0x45); //correct write
            Assert.AreEqual(ram.Read(0x08C0), 0x45); //correct mirrored write
            Assert.AreEqual(ram.Read(0x10C0), 0x45); //correct mirrored write
            Assert.AreEqual(ram.Read(0x18C0), 0x45); //correct mirrored write
        }
    }
}

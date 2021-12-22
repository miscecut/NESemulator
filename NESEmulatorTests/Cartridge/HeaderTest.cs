using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator.Cartridge;

namespace NESEmulatorTests.Cartridge
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void TestHeader()
        {
            var headerBytes = new byte[]
            {
                0x00,0x00,0x00,0x10, //name
                0x01, //size of PRG ROM
                0x02, //size of CHR ROM
                0b01001111, //mapper flags 1 - trainer present
                0b11010001, //mapper flags 2
                0b00001100, //PRG RAM flags
                0b11110111, //TV systems flags 1
                0b10010111, //TV systems flags 2
                0xFF,0xFF,0xFF,0xFF,0xFF //unused bytes
            };
            var header = new CartridgeHeader(headerBytes);
            Assert.AreEqual(header.GetMapperId(), 0b11010100); //first nibble of MFs 2 | first nibble of MFs 1
            Assert.AreEqual(header.SizeOfCharacterROM, 2);
            Assert.AreEqual(header.SizeOfProgramROM, 1);
            Assert.IsTrue(header.IsTrainerPresent());
        }
    }
}

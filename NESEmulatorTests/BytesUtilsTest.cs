using Microsoft.VisualStudio.TestTools.UnitTesting;
using NESEmulator;

namespace NESEmulatorTests
{
    [TestClass]
    public class BytesUtilsTest
    {
        [TestMethod]
        public void TestByteUnion()
        {
            byte hi = 0xB4;
            byte lo = 0x3F;
            Assert.AreEqual(0xB43F, BytesUtils.CombineBytes(hi, lo));
            hi = 0x00;
            lo = 0x02;
            Assert.AreEqual(0x0002, BytesUtils.CombineBytes(hi, lo));
            hi = 0x86;
            lo = 0xAA;
            Assert.AreEqual(0x86AA, BytesUtils.CombineBytes(hi, lo));
        }

        [TestMethod]
        public void TestHiByteRetrieving()
        {
            ushort address = 0x04EB;
            Assert.AreEqual(0x04, BytesUtils.GetHiByte(address));
            address = 0x1000;
            Assert.AreEqual(0x10, BytesUtils.GetHiByte(address));
        }

        [TestMethod]
        public void TestLoByteRetrieving()
        {
            ushort address = 0x113B;
            Assert.AreEqual(0x3B, BytesUtils.GetLoByte(address));
            address = 0x13E0;
            Assert.AreEqual(0xE0, BytesUtils.GetLoByte(address));
        }

        [TestMethod]
        public void TestZeroPageSum()
        {
            byte byte1 = 0b00011110;
            byte byte2 = 0b00000001;
            Assert.AreEqual(0b0000000000011111, BytesUtils.ZeroPageSum(byte1, byte2));
            byte1 = 0b11111111;
            byte2 = 0b00000001;
            Assert.AreEqual(0x0000, BytesUtils.ZeroPageSum(byte1, byte2));
            byte1 = 0b11111111;
            byte2 = 0b00000010;
            Assert.AreEqual(0x0001, BytesUtils.ZeroPageSum(byte1, byte2));
        }

        [TestMethod]
        public void TestMSB()
        {
            Assert.IsTrue(BytesUtils.GetMSB(0b10000111));
            Assert.IsFalse(BytesUtils.GetMSB(0b00000111));
        }
    }
}

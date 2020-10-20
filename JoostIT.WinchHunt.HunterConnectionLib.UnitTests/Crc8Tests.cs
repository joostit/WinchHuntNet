using JoostIT.WinchHunt.HunterConnectionLib.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JoostIT.WinchHunt.HunterConnectionLib.UnitTests
{
    [TestClass]
    public class Crc8Tests
    {
        [TestMethod]
        public void TestEmptyStringReturnsFF()
        {
            byte value = Crc8.CalculateCrc("");

            Assert.AreEqual(0xFF, value);
        }

        [TestMethod]
        public void TestNullStringThrowsArgumentNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Crc8.CalculateCrc(null));
        }



        // ToDo: Add additional tests for CRC values
    }
}

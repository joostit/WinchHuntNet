using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JoostIT.WinchHunt.WinchHuntNet.UnitTests
{
    [TestClass]
    public class Crc8Tests
    {
        
        private const string crcString = "TZCWNCH1BAC6432e4{\"gps\":{\"lat\":0,\"lon\":0,\"sats\":0,\"spd\":0,\"alt\":0,\"hdop\":99.99,\"fix\":false},\"dev\":{\"hw\":\"T22_V1.1\",\"id\":\"1BAC64\",\"name\":\"Proto 1\",\"v\":1}}";
        private const byte crcByte = 0x83;


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


        [TestMethod]
        public void TestSpecificStringReturnsSpecificCrc()
        {
            byte value = Crc8.CalculateCrc(crcString);
            Assert.AreEqual(crcByte, value);
        }

    }
}

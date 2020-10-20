using JoostIT.WinchHunt.WinchHuntConnectionLib.LoraMessaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.UnitTests
{
    [TestClass]
    public class LoraPacketBuilderTests
    {

        private const string MagicWordString = "TZCWNCH";
        private const string SenderIdString = "AABBCC";
        private const string MessageIdString = "1122";
        private const string JsonData = "{JSONJSONJSON}";
        private const string ValidCrcString = "14";

        private const string InvalidCrcString = "BC";

        private uint SenderId = Convert.ToUInt32(SenderIdString, 16);
        private uint MessageId = Convert.ToUInt32(MessageIdString, 16);
        private uint Crc = Convert.ToUInt32(ValidCrcString, 16);

        private const string ValidPacketString = MagicWordString + SenderIdString + MessageIdString + JsonData + ValidCrcString;
        private const string InvalidPacketString = MagicWordString + SenderIdString + MessageIdString + JsonData + InvalidCrcString;


        [TestMethod]
        public void InputNullThrowsArgumentNull()
        {
            LoraPacketBuilder builder = new LoraPacketBuilder();

            Assert.ThrowsException<ArgumentNullException>(() => builder.CreatePacket(null));
        }


        [TestMethod]
        public void InputTooShortThrowsInvalidData()
        {
            LoraPacketBuilder builder = new LoraPacketBuilder();

            Assert.ThrowsException<InvalidDataException>(() => builder.CreatePacket(""));                       // 0
            Assert.ThrowsException<InvalidDataException>(() => builder.CreatePacket("1"));                      // 1
            Assert.ThrowsException<InvalidDataException>(() => builder.CreatePacket("123456789012345678"));     // 18
        }


        [TestMethod]
        public void TestValidPacketDissection()
        {
            LoraPacketBuilder builder = new LoraPacketBuilder();

            LoraPacket result = builder.CreatePacket(ValidPacketString);

            Assert.AreEqual(SenderId, result.SenderId);
            Assert.AreEqual(MessageId, result.MessageId);
            Assert.AreEqual(Crc, result.Crc);
            Assert.AreEqual(JsonData, result.JsonData);
        }
    }
}

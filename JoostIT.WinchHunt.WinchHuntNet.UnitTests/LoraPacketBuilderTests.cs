using JoostIT.WinchHunt.WinchHuntNet.LoraMessaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.UnitTests
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
        public void InputTooShortGivesErrorResult()
        {
            LoraPacketBuilder builder = new LoraPacketBuilder();

            PacketParseResult<LoraPacket> result1 = builder.CreatePacket("");
            PacketParseResult<LoraPacket> result2 = builder.CreatePacket("1");
            PacketParseResult<LoraPacket> result3 = builder.CreatePacket("123456789012345678");

            Assert.IsFalse(result1.IsValid);
            Assert.IsFalse(result2.IsValid);
            Assert.IsFalse(result3.IsValid);

        }


        [TestMethod]
        public void TestValidPacketDissection()
        {
            LoraPacketBuilder builder = new LoraPacketBuilder();

            LoraPacket result = builder.CreatePacket(ValidPacketString).Result;

            Assert.AreEqual(SenderId, result.SenderId);
            Assert.AreEqual(MessageId, result.MessageId);
            Assert.AreEqual(Crc, result.Crc);
            Assert.AreEqual(JsonData, result.JsonData);
        }
    }
}

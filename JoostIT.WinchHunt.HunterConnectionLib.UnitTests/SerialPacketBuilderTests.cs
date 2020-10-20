using JoostIT.WinchHunt.HunterConnectionLib.SerialConnection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.UnitTests
{
    [TestClass]
    public class SerialPacketBuilderTests
    {

        [TestMethod]
        public void TestBuilderStartsAtIdle()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();
            
            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);
        }


        [TestMethod]
        public void TestNoPacketAtIdle()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();
            builder.Reset();
            Assert.IsNull(builder.CurrentPacket);
        }


        [TestMethod]
        public void TestIgnoreGarbage()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("KSNVT/^CVAKJC");

            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);
        }


        [TestMethod]
        public void TestDetectFirstStartSequenceChar()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W");

            Assert.AreEqual(SerialRxStates.ReadingStart, builder.RxState);
        }


        [TestMethod]
        public void TestInvalidCharInStartSequenceSetsToIdle()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("Wq");

            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);
        }


        [TestMethod]
        public void TestValidStartSequenceSetsToReadingType()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_");

            Assert.AreEqual(SerialRxStates.ReadingType, builder.RxState);
        }


        [TestMethod]
        public void TestValidTypeFieldSetsToReadingLength()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR");

            Assert.AreEqual(SerialRxStates.ReadingLength, builder.RxState);
        }


        [TestMethod]
        public void TestLRTypeFieldSetsLoraRxType()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR");

            Assert.AreEqual(SerialPacketTypes.LoraRx, builder.CurrentPacket.PacketType);
        }


        [TestMethod]
        public void TestHBTypeFieldSetHeartBeatType()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_HB");

            Assert.AreEqual(SerialPacketTypes.HeartBeat, builder.CurrentPacket.PacketType);
        }


        [TestMethod]
        public void TestInvalidTypeFieldResetsToIdle()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_xx");

            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);
        }



        [TestMethod]
        public void TestValidlengthFieldSetsReadingData()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_HB1234");

            Assert.AreEqual(SerialRxStates.ReadingData, builder.RxState);
        }


        [TestMethod]
        public void TestValidlengthFieldValue()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR1234");

            Assert.AreEqual(1234, builder.CurrentPacket.DataLength);
        }


        [TestMethod]
        public void TestZerolengthSkipsToEndSequence()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR0000");

            Assert.AreEqual(SerialRxStates.ReadingEnd, builder.RxState);
        }


        [TestMethod]
        public void TestZerolengthCreatesEmptyDataString()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR0000");

            Assert.AreEqual(String.Empty, builder.CurrentPacket.Data);
        }


        [TestMethod]
        public void TestInvalidlengthFieldValueSetsToIdle()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR12Z4");

            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);
        }


        [TestMethod]
        public void TestFullDataReadSetsToReadingEndSequence()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR0005ABCDE");

            Assert.AreEqual(SerialRxStates.ReadingEnd, builder.RxState);
        }


        [TestMethod]
        public void TestFullDataReadSavesDataInCurrentPacket()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_LR0005ABCDE");

            Assert.AreEqual("ABCDE", builder.CurrentPacket.Data);
        }


        [TestMethod]
        public void TestFullValidPacketRaisesNewPacketEvent()
        {
            int eventCount = 0;

            SerialPacketBuilder builder = new SerialPacketBuilder();
            builder.PacketReceived += (s, e) =>
            {
                eventCount++;
                Assert.AreEqual(5, e.Packet.DataLength);
                Assert.AreEqual("ABCDE", e.Packet.Data);
                Assert.AreEqual(SerialPacketTypes.LoraRx, e.Packet.PacketType);
            };
            

            builder.ProcessSerialData("W#L_LR0005ABCDE_H^\n");

            Assert.AreEqual(1, eventCount);
            
        }


        [TestMethod]
        public void TestValidPacketWithInvalidEndSequenceResetsToIdle()
        {
            int eventCount = 0;

            SerialPacketBuilder builder = new SerialPacketBuilder();
            builder.PacketReceived += (s, e) =>
            {
                eventCount++;
            };

            builder.ProcessSerialData("W#L_LR0005ABCDE_H^x");

            Assert.AreEqual(0, eventCount);
            Assert.AreEqual(SerialRxStates.Idle, builder.RxState);

        }


    }
}

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
        public void TestValidStartSequenceSetsToReadingLength()
        {
            SerialPacketBuilder builder = new SerialPacketBuilder();

            builder.ProcessSerialData("W#L_");

            Assert.AreEqual(SerialRxStates.ReadingLength, builder.RxState);
        }


    }
}

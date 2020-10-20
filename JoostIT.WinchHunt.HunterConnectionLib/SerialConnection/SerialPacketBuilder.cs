using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    internal class SerialPacketBuilder
    {

        private const int HexBase = 16;

        internal SerialRxStates RxState { get; private set; } = SerialRxStates.Idle;

        private SerialPacket currentPacket;
        private StringBuilder fieldRxBuffer;


        public event EventHandler<NewSerialPacketEventArgs> PacketReceived;


        /// <summary>
        /// Processes new Serial data to find packets. New packets will be raised via the PacketReceived event
        /// </summary>
        /// <param name="data">Newly received data from the serial port</param>
        public void ProcessSerialData(String data)
        {
            foreach(var character in data)
            {
                ProcessCharacter(character);
            }
        }


        public void Reset()
        {
            SetRxState(SerialRxStates.Idle);
        }

        
        private void RaisePacketReceived(SerialPacket packet)
        {
            PacketReceived?.Invoke(this, new NewSerialPacketEventArgs(packet));
        }

        private void ProcessCharacter(char character)
        {
            switch (RxState)
            {
                case SerialRxStates.Idle:
                    DetectStartSequenceStart(character);
                    break;

                case SerialRxStates.ReadingStart:
                    ReceiveStartSequence(character);
                    break;

                case SerialRxStates.ReadingLength:
                    break;

                case SerialRxStates.ReadingData:
                    break;

                case SerialRxStates.ReadingEnd:
                    break;

                default:
                    throw new NotSupportedException($"Unsupported Serial Rx State: SerialRxStates.{RxState}");
            }
        }

        private void DetectStartSequenceStart(char character)
        {
            if (character.Equals(SerialPacket.StartSequence[0]))
            {
                fieldRxBuffer = new StringBuilder();
                fieldRxBuffer.Append(character);
                SetRxState(SerialRxStates.ReadingStart);
            }
        }


        private void ReceiveStartSequence(char character)
        {
            if (fieldRxBuffer.Length >= SerialPacket.StartSequence.Length) { throw new ConstraintException("Bug: Trying to find more Start sequence characters than the sequence length itself"); }

            if (character.Equals(SerialPacket.StartSequence[fieldRxBuffer.Length]))
            {
                fieldRxBuffer.Append(character);

                if(fieldRxBuffer.Length == SerialPacket.StartSequence.Length)
                {
                    SetRxState(SerialRxStates.ReadingLength);
                }
            }
            else
            {
                SetRxState(SerialRxStates.Idle);
            }
        }


        private void SetRxState(SerialRxStates newState)
        {
            RxState = newState;
        }

    }
}

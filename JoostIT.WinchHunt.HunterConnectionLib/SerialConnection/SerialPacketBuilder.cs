using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    internal class SerialPacketBuilder
    {
        public SerialRxStates RxState { get; private set; } = SerialRxStates.Idle;

        internal SerialPacket CurrentPacket { get; private set; } = null;
        private StringBuilder fieldRxBuffer;


        public event EventHandler<NewSerialPacketEventArgs> PacketReceived;


        public SerialPacketBuilder()
        {
            Reset();
        }

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


        /// <summary>
        /// Resets the builder by resetting the internal state machine to Idle
        /// </summary>
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
                    ReceiveLengthChars(character);
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
                SetRxState(SerialRxStates.ReadingStart);
                fieldRxBuffer.Append(character);
            }
        }


        private void ReceiveStartSequence(char character)
        {
            // This would be a bug
            if (fieldRxBuffer.Length >= SerialPacket.StartSequence.Length) { throw new ConstraintException("Bug: Trying to find more Start sequence characters than the sequence length itself"); }

            if (character.Equals(SerialPacket.StartSequence[fieldRxBuffer.Length]))
            {
                fieldRxBuffer.Append(character);

                // If we have enough valid characters in a row, we move on to the next field
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


        private void ReceiveLengthChars(char character)
        {
            if (fieldRxBuffer.Length >= SerialPacket.LengthFieldLength) { throw new ConstraintException("Bug: Trying to read more length characters than the Length field length itself"); }

            fieldRxBuffer.Append(character);

            if(fieldRxBuffer.Length == SerialPacket.LengthFieldLength)
            {
                

                try
                {
                    CurrentPacket.DataLength = Convert.ToInt32(fieldRxBuffer.ToString());
                    SetRxState(SerialRxStates.ReadingData);
                }
                catch(FormatException)
                {
                    // In case the characters cannot be pased to an int the packet is corrupt and should be ignored
                    SetRxState(SerialRxStates.Idle);
                }
            }

        }


        private void SetRxState(SerialRxStates newState)
        {
            RxState = newState;

            fieldRxBuffer = new StringBuilder();

            // We don't have a packet while in Idle, until we have fully received a StartSequence.
            if (newState == SerialRxStates.Idle)
            {
                CurrentPacket = null;
            }

            // Now we're actually starting to have a packet
            if (newState == SerialRxStates.ReadingLength)
            {
                CurrentPacket = new SerialPacket();
            }
        }


    }
}

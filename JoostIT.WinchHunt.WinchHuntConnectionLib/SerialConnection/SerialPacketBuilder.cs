using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntConnectionLib.SerialConnection
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

                case SerialRxStates.ReadingType:
                    ReceiveTypeChars(character);
                    break;

                case SerialRxStates.ReadingLength:
                    ReceiveLengthChars(character);
                    break;

                case SerialRxStates.ReadingData:
                    ReceiveData(character);
                    break;

                case SerialRxStates.ReadingEnd:
                    ReceiveEndSequence(character);
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
                    
                    SetRxState(SerialRxStates.ReadingType);
                }
            }
            else
            {
                SetRxState(SerialRxStates.Idle);
            }
        }


        private void ReceiveTypeChars(char character)
        {
            if (fieldRxBuffer.Length >= SerialPacket.TypeFieldLength) { throw new ConstraintException("Bug: Trying to read more type characters than the type field length is "); }

            fieldRxBuffer.Append(character);

            if (fieldRxBuffer.Length == SerialPacket.TypeFieldLength)
            {
                switch (fieldRxBuffer.ToString())
                {
                    case "LR":
                        CurrentPacket.PacketType = SerialPacketTypes.LoraRx;
                        SetRxState(SerialRxStates.ReadingLength);
                        break;

                    case "HB":
                        CurrentPacket.PacketType = SerialPacketTypes.HeartBeat;
                        SetRxState(SerialRxStates.ReadingLength);
                        break;

                    default:
                        SetRxState(SerialRxStates.Idle);
                        break;
                }
               
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
                    if (CurrentPacket.DataLength > 0)
                    {
                        SetRxState(SerialRxStates.ReadingData);
                    }
                    else
                    {
                        // If this packet has zero length data, skip reading it.
                        CurrentPacket.Data = String.Empty;
                        SetRxState(SerialRxStates.ReadingEnd);
                    }
                }
                catch(FormatException)
                {
                    // In case the characters cannot be pased to an int the packet is corrupt and should be ignored
                    SetRxState(SerialRxStates.Idle);
                }
            }
        }


        private void ReceiveData(char character)
        {
            if (fieldRxBuffer.Length >= CurrentPacket.DataLength) { throw new ConstraintException("Bug: Trying to read more data characters than the Length field indicates"); }

            fieldRxBuffer.Append(character);

            if(fieldRxBuffer.Length == CurrentPacket.DataLength)
            {
                CurrentPacket.Data = fieldRxBuffer.ToString();
                SetRxState(SerialRxStates.ReadingEnd);
            }
        }


        private void ReceiveEndSequence(char character)
        {
            if (fieldRxBuffer.Length >= SerialPacket.EndSequence.Length) { throw new ConstraintException("Bug: Trying to find more End sequence characters than the sequence length itself"); }

            if (character.Equals(SerialPacket.EndSequence[fieldRxBuffer.Length]))
            {
                fieldRxBuffer.Append(character);

                // If we have enough valid characters in a row, we have a complete and valid packet
                if (fieldRxBuffer.Length == SerialPacket.EndSequence.Length)
                {
                    RaisePacketReceived(CurrentPacket);
                    SetRxState(SerialRxStates.Idle);
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

            fieldRxBuffer = new StringBuilder();

            // We don't have a packet while in Idle, until we have fully received a StartSequence.
            if (newState == SerialRxStates.Idle)
            {
                CurrentPacket = null;
            }

            // Now we're actually starting to have a packet
            if (newState == SerialRxStates.ReadingType)
            {
                CurrentPacket = new SerialPacket();
            }
        }


    }
}

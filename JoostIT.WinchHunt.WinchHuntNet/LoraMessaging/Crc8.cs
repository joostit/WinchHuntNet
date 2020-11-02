using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.LoraMessaging
{
    internal static class Crc8
    {

        internal static byte CalculateCrc(String input)
        {
            if (input == null) { throw new ArgumentNullException("input"); }

            byte[] ascii = Encoding.ASCII.GetBytes(input);

            byte crc = 0xff;

            for (int inputIndex = 0; inputIndex < input.Length; inputIndex++)
            {
                crc ^= ascii[inputIndex];
                for (int bitIndex = 0; bitIndex < 8; bitIndex++)
                {
                    if ((crc & 0x80) != 0)
                        crc = (byte)((crc << 1) ^ 0x31);
                    else
                        crc <<= 1;
                }
            }

            return crc;
        }

    }
}

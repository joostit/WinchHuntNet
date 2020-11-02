using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet.SerialConnection
{
    internal enum SerialRxStates
    {
        Idle,
        ReadingStart,
        ReadingType,
        ReadingLength,
        ReadingData,
        ReadingEnd
    }
}

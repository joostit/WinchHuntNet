﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.HunterConnectionLib.SerialConnection
{
    internal enum SerialRxStates
    {
        Idle,
        ReadingStart,
        ReadingLength,
        ReadingData,
        ReadingEnd
    }
}

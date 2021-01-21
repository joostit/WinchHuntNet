using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Holds the result of a packet parsing operation
    /// </summary>
    /// typeparam name="TPacket">The result packet type
    public class PacketParseResult<TPacket> : ParseResult
    {

        
        /// <summary>
        /// The result package. Null if the parsing was unsuccessful
        /// </summary>
        public TPacket Result { get; private set; }


        /// <summary>
        /// Constructor. Creates a valid PacketParseResult with the provided package
        /// </summary>
        /// <param name="result"></param>
        public PacketParseResult(TPacket result)
        {
            this.Result = result;
            IsValid = true;
        }

        /// <summary>
        /// Constructor. Creates an invalid PacketParseResult with the provided exception
        /// </summary>
        /// <param name="exception"></param>
        public PacketParseResult(Exception exception)
            :base(exception)
        {
        }

        /// <summary>
        /// Constructor. Creates an invalid PacketParseResult with the provided error message
        /// </summary>
        /// <param name="errorMessage"></param>
        public PacketParseResult(string errorMessage)
            :base(errorMessage)
        {
          
        }


        /// <summary>
        /// Returns a String representation of the state of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsValid && (Result != null))
            {
                return Result.ToString();
            }
            else
            {
                return base.ToString();
            }
        }

    }
}

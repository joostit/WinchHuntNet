using System;
using System.Collections.Generic;
using System.Text;

namespace JoostIT.WinchHunt.WinchHuntNet
{

    /// <summary>
    /// Non-generic base class for a packet parse result
    /// </summary>
    public abstract class ParseResult
    {

        private string errorMessage = "";

        /// <summary>
        /// Returns true if this object holds a valid result object. Otherwise false
        /// </summary>
        public bool IsValid { get; protected set; }


        /// <summary>
        /// Gets the Exception that occured during parsing, if available. Otherwise null.
        /// </summary>
        public Exception ParseException { get; private set; }

        /// <summary>
        /// Returns true if this object has a ParseException associtated with it. 
        /// </summary>
        public bool HasException { get => ParseException != null; }

        /// <summary>
        /// Gets the error message that resulted from the parsing operation, if available.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (ParseException != null)
                {
                    return ParseException.Message;
                }
                else
                {
                    return errorMessage;
                }
            }
            private set
            {
                errorMessage = value;
            }
        }

        /// <summary>
        /// Constructor. Creates a valid Parse result
        /// </summary>
        public ParseResult()
        {
            IsValid = true;
        }


        /// <summary>
        /// Constructor. Creates an invalid ParseResult with the provided exception
        /// </summary>
        /// <param name="exception"></param>
        public ParseResult(Exception exception)
        {
            this.ParseException = exception;
            IsValid = false;
        }

        /// <summary>
        /// Constructor. Creates an invalid ParseResult with the provided error message
        /// </summary>
        /// <param name="errorMessage"></param>
        public ParseResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsValid = false;
        }


        /// <summary>
        /// Returns a string representation of the current state of this object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (IsValid)
            {
                return "Valid parse result";
            }

            if (!String.IsNullOrWhiteSpace(ErrorMessage))
            {
                return $"Invalid ParseResult: {ErrorMessage}";
            }

            return base.ToString();
        }


    }
}

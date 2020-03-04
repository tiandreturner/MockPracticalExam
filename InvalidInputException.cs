using System;

namespace MockPracticalExam
{
    class InvalidInputException : Exception
    {
        string message;

        public override string Message { get{ return message; } }
        public InvalidInputException(string message)
        {
            this.message = $"Error -- {message}";

        }
    }
}

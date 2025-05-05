using System;

namespace GeoGuessrWinForms.Logic.Exceptions
{
    public class NoAvailableLocationsException : Exception
    {
        public NoAvailableLocationsException()
            : base("No avaible locations for game.") { }

        public NoAvailableLocationsException(string message)
            : base(message) { }

        public NoAvailableLocationsException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

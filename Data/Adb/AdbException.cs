namespace EveMagic.Data.Adb
{
    using System;

    public class AdbException : Exception
    {
        internal AdbException(string message) : base(message)
        {
        }
    }

    public class AdbInvalidResponseException : AdbException
    {
        internal AdbInvalidResponseException(string response) : base($"The server returned an invalid response '{response}'")
        {
        }
    }
}

using System.Runtime.Serialization;

namespace casino.Models;

public class CardException : Exception
{
    public CardException()
    {
    }

    protected CardException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CardException(string? message) : base(message)
    {
    }

    public CardException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
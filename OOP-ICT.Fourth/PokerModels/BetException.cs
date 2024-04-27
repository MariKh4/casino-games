using System.Runtime.Serialization;

namespace casino.PokerModels;

public class BetException : Exception
{
    public BetException()
    {
    }

    protected BetException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public BetException(string? message) : base(message)
    {
    }

    public BetException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
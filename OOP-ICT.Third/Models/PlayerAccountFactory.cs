using casino.Interfaces;

namespace casino.Models;

public class PlayerAccountFactory
{
    public IPlayerAccount CreatePlayerAccount(decimal initialBalance)
    {
        return new PlayerAccount(initialBalance);
    }
}
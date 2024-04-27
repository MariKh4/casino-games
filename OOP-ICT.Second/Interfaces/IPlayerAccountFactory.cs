namespace casino.Interfaces;

public interface IPlayerAccountFactory
{
    IPlayerAccount CreatePlayerAccount(decimal initialBalance);
}
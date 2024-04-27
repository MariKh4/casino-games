namespace casino.Interfaces;

public interface IPlayerAccount
{
    decimal Balance { get; }
    void Deposit(decimal amount);
    bool Withdraw(decimal amount);
}
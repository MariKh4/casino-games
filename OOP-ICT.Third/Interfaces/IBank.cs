namespace casino.Interfaces;

public interface IBank
{
    void Deposit(IPlayerAccount playerAccount, decimal amount);
    bool Withdraw(IPlayerAccount playerAccount, decimal amount);
    bool HasSufficientFunds(IPlayerAccount playerAccount, decimal amount);
}
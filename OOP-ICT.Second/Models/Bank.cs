using casino.Interfaces;

namespace casino.Models;

public class Bank : IBank
{
    public void Deposit(IPlayerAccount playerAccount, decimal amount)
    {
        playerAccount.Deposit(amount);
    }

    public bool Withdraw(IPlayerAccount playerAccount, decimal amount)
    {
        return playerAccount.Withdraw(amount);
    }

    public bool HasSufficientFunds(IPlayerAccount playerAccount, decimal amount)
    {
        return playerAccount.Balance >= amount;
    }
}
using casino.Interfaces;

namespace casino.PokerModels;

public class PokerBank : IBank
{
    public PokerBank(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public decimal Balance { get; private set; }

    public void Deposit(IPlayerAccount playerAccount, decimal amount)
    {
        Balance += amount;
        playerAccount.Deposit(amount);
    }

    public bool Withdraw(IPlayerAccount playerAccount, decimal amount)
    {
        if (!HasSufficientFunds(playerAccount, amount)) return false;
        
        Balance -= amount;
        playerAccount.Withdraw(amount);
        return true;
    }

    public bool HasSufficientFunds(IPlayerAccount playerAccount, decimal amount)
    {
        return playerAccount.Balance >= amount;
    }
}
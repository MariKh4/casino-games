using casino.Interfaces;

namespace casino.Models;

public class PlayerAccount : IPlayerAccount
{
    public decimal Balance { get; private set; }

    public PlayerAccount(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > Balance) return false;
        Balance -= amount;
        return true;
    }
}
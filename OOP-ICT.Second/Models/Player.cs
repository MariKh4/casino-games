using casino.Interfaces;

namespace casino.Models;

public class Player
{
    public Player(IPlayerAccount playerAccount)
    {
        Account = playerAccount;
    }

    public IPlayerAccount Account { get; }
    public Hand Hand { get; private set; } = new();
    public decimal CurrentBet { get; private set; }

    public void PlaceBet(decimal amount)
    {
        if (amount > 0 && Account.Withdraw(amount))
        {
            CurrentBet = amount;
        }
    }

    public void ClearHand()
    {
        Hand = new Hand();
    }
}
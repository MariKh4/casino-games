using casino.Interfaces;

namespace casino.Models;

public class DealerTurnStrategy : ITurnStrategy
{
    private readonly Func<string, bool> _userPrompt;
    private readonly Dealer _dealer;

    public DealerTurnStrategy(Func<string, bool> userPrompt, Dealer dealer)
    {
        _userPrompt = userPrompt;
        _dealer = dealer;
    }

    public void PerformTurn(Hand hand)
    {
        while (hand.CalculateHandValue() < 17)
        {
            hand.AddCard(_dealer.DealCards(1).First());
        }
    }
}
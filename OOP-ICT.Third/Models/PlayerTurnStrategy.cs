using casino.Interfaces;

namespace casino.Models;

public class PlayerTurnStrategy : ITurnStrategy
{
    private readonly Func<string, bool> _userPrompt;
    private readonly Dealer _dealer;

    public PlayerTurnStrategy(Func<string, bool> userPrompt, Dealer dealer)
    {
        _userPrompt = userPrompt;
        _dealer = dealer;
    }

    public void PerformTurn(Hand hand)
    {
        while (hand.CalculateHandValue() < 21 && _userPrompt.Invoke(""))
        {
            hand.AddCard(_dealer.DealCards(1).First());
        }
    }
}
using Microsoft.VisualBasic;
using casino.Interfaces;

namespace casino.Models;

public class BlackjackCasino : IBlackjackCasino
{
    private readonly IBank _bank;

    public BlackjackCasino(IBank bank)
    {
        _bank = bank;
    }

    public void PayWinning(Player player, decimal amount)
    {
        _bank.Deposit(player.Account, amount);
    }

    public void ChargeLoss(Player player, decimal amount)
    {
        _bank.Withdraw(player.Account, amount);
    }

    public void ProcessBlackjack(Player player)
    {
        var hand = player.Hand;

        if (hand.Cards.Count == 2 && IsBlackjack(hand))
        {
            PayBlackjackWinning(player);
        }
    }

    private bool IsBlackjack(Hand hand)
    {
        return hand.Cards.Sum(GetCardValue) == 21 && ContainsAce(hand);
    }

    private bool ContainsAce(Hand hand)
    {
        return hand.Cards.Any(card => card.Rank == Rank.Ace);
    }
 
    private int GetCardValue(Card card)
    {
        return card.Rank == Rank.Ace ? 11 : (int)card.Rank;
    }

    private void PayBlackjackWinning(Player player)
    {
        var winningAmount = player.CurrentBet * 2;
        PayWinning(player, winningAmount);
    }
}
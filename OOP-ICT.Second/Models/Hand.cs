namespace casino.Models;

public class Hand
{
    private readonly List<Card> _cards = new();

    public IReadOnlyCollection<Card> Cards => _cards;

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public int CalculateHandValue()
    {
        var aceCount = 0;

        var value = _cards.Sum(card => GetCardValue(card, ref aceCount));

        while (aceCount > 0 && value > 21)
        {
            value -= 10;
            aceCount--;
        }

        return value;
    }

    private int GetCardValue(Card card, ref int aceCount)
    {
        switch (card.Rank)
        {
            case Rank.Ace:
                aceCount++;
                return 11;
            case Rank.King:
                return 10;
            case Rank.Jack:
            case Rank.Queen:
            case Rank.Two:
            case Rank.Three:
            case Rank.Four:
            case Rank.Five:
            case Rank.Six:
            case Rank.Seven:
            case Rank.Eight:
            case Rank.Nine:
            case Rank.Ten:
            default:
                return (int)card.Rank;
        }
    }
}
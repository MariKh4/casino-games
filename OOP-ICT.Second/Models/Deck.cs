namespace casino.Models;

public class Deck
{
    private readonly List<Card> _cards = new();
    private int _currentIndex = 0;

    public Deck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                _cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        var random = new Random();
        var n = _cards.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }
    }

    public List<Card> GetShuffledDeck()
    {
        return _cards;
    }

    public Card DrawCard()
    {
        if (_cards.Count == 0)
        {
            throw new CardException();
        }

        var card = _cards[0];
        _cards.Remove(card);
        return card;
    }
}
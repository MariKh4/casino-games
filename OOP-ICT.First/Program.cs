namespace casino.Models;

namespace casino;

internal abstract class Program
{
    private static void Main()
    {
        var dealer = new Dealer();
        
        var hand = dealer.DealCards(5);

        foreach (var card in hand)
        {
            Console.WriteLine($"{card.Rank} of {card.Suit}");
        }
    }
}
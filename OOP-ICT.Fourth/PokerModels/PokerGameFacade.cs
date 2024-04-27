using casino.PokerInterfaces;
using casino.Models;

namespace casino.PokerModels;

public class PokerGameFacade
{
    private readonly Dictionary<Player, IBettingStrategy> _bettingStrategyByPlayer = new();

    public PokerGameFacade(IPokerCasino pokerCasino, IDealer dealer)
    {
        PokerCasino = pokerCasino;
        Dealer = dealer;
    }

    public IPokerCasino PokerCasino { get; }

    public IReadOnlyCollection<Player> Players => _bettingStrategyByPlayer.Keys;

    public IDealer Dealer { get; }

    public void AddPlayer(Player player, IBettingStrategy bettingStrategy)
    {
        _bettingStrategyByPlayer.Add(player, bettingStrategy);
    }

    public void StartNewGame()
    {
        foreach (var player in Players)
        {
            player.ClearHand();
        }
    }

    public void DealCards()
    {
        const int cardsPerPlayer = 5;

        foreach (var player in _bettingStrategyByPlayer)
        {
            for (var i = 0; i < cardsPerPlayer; i++)
            {
                player.Key.Hand.AddCard(Dealer.DealCards(1).First());
            }
        }
    }

    public void CollectBets()
    {
        foreach (var player in _bettingStrategyByPlayer)
        {
            var betAmount = player.Value.GetBet();
            player.Key.PlaceBet(betAmount);
        }
    }

    public void ReplacePlayerCards(Player player, List<int> cardIndicesToReplace)
    {
        foreach (var index in cardIndicesToReplace)
        {
            player.Hand.ReplaceCard(index, Dealer);
        }
    }

    public void DetermineWinnerAndSettleAccounts()
    {
        var winner = DetermineWinner();

        PokerCasino.PayWinning(winner, CalculateWinnings());

        foreach (var player in Players.Except(new[] { winner }))
        {
            PokerCasino.ChargeLoss(player, player.CurrentBet);
        }
    }

    private Player DetermineWinner()
    {
        return Players.MaxBy(player => new PokerHandEvaluator().EvaluateHand(player.Hand)) ?? throw new CardException();
    }

    private decimal CalculateWinnings()
    {
        return Players.Sum(player => player.CurrentBet);
    }
}
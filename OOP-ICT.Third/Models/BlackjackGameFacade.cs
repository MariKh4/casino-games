using casino.Interfaces;

namespace casino.Models;

public class BlackjackGameFacade
{
    private readonly List<Player> _players;

    public BlackjackGameFacade(IBank bank)
    {
        Dealer = new Dealer();
        DealersHand = new Hand();

        _players = new List<Player>();
        BlackjackCasino = new BlackjackCasino(bank);
    }

    public Dealer Dealer { get; }

    public IReadOnlyCollection<Player> Players => _players;

    public IBlackjackCasino BlackjackCasino { get; }

    public Hand DealersHand { get; private set; }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public void PlaceBets(decimal betAmount)
    {
        foreach (var player in _players)
        {
            player.PlaceBet(betAmount);
        }
    }

    public void DealInitialCards()
    {
        const int numOfCardsDeal = 2;

        foreach (var player in _players)
        {
            for (var i = 0; i < numOfCardsDeal; i++)
            {
                player.Hand.AddCard(Dealer.DealCards(1).First());
            }
        }

        DealersHand.AddCard(Dealer.DealCards(1).First());
    }

    public void PlayRound(ITurnStrategy playerTurnStrategy, ITurnStrategy dealerTurnStrategy)
    {
        DealInitialCards();

        foreach (var player in _players)
        {
            playerTurnStrategy.PerformTurn(player.Hand);
        }

        dealerTurnStrategy.PerformTurn(DealersHand);

        DetermineWinnersAndLosers();

        ClearHands();
    }

    private void DetermineWinnersAndLosers()
    {
        foreach (var player in _players)
        {
            if (player.Hand.CalculateHandValue() > 21)
            {
                BlackjackCasino.ChargeLoss(player, player.CurrentBet);
            }
            else if (DealersHand.CalculateHandValue() > 21 ||
                     player.Hand.CalculateHandValue() > DealersHand.CalculateHandValue())
            {
                BlackjackCasino.PayWinning(player, player.CurrentBet * 2);
            }
            else if (player.Hand.CalculateHandValue() == DealersHand.CalculateHandValue())
            {
                continue;
            }
            else
            {
                BlackjackCasino.ChargeLoss(player, player.CurrentBet);
            }
        }
    }

    private void ClearHands()
    {
        foreach (var player in _players)
        {
            player.ClearHand();
        }

        DealersHand = new Hand();
    }
}
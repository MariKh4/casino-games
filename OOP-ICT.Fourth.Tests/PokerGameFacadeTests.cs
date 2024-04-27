using Moq;
using casino.PokerInterfaces;
using casino.PokerModels;
using casino.Models;
using Xunit;

namespace casino.Tests;

public class PokerGameFacadeTests
{
    [Fact]
    public void StartNewGame_ShouldClearHandsOfAllPlayers()
    {
        var pokerCasinoMock = new Mock<IPokerCasino>();
        var facade = new PokerGameFacade(pokerCasinoMock.Object, new IDealer());
        var player1 = new Player(new PlayerAccount(100));
        var player2 = new Player(new PlayerAccount(100));
        facade.AddPlayer(player1, Mock.Of<IBettingStrategy>());
        facade.AddPlayer(player2, Mock.Of<IBettingStrategy>());

        facade.StartNewGame();

        Assert.Empty(player1.Hand.Cards);
        Assert.Empty(player2.Hand.Cards);
    }

    [Fact]
    public void DealCards_ShouldDealCorrectNumberOfCardsToEachPlayer()
    {
        var pokerCasinoMock = new Mock<IPokerCasino>();
        var dealerMock = new Mock<IDealer>();
        var facade = new PokerGameFacade(pokerCasinoMock.Object, dealerMock.Object);
        var player1 = new Player(new PlayerAccount(100));
        var player2 = new Player(new PlayerAccount(100));
        facade.AddPlayer(player1, Mock.Of<IBettingStrategy>());
        facade.AddPlayer(player2, Mock.Of<IBettingStrategy>());

        facade.DealCards();

        Assert.Equal(5, player1.Hand.Cards.Count);
        Assert.Equal(5, player2.Hand.Cards.Count);
    }

    [Fact]
    public void CollectBets_ShouldPlaceCorrectBetsForAllPlayers()
    {
        // Arrange
        var pokerCasinoMock = new Mock<IPokerCasino>();
        var facade = new PokerGameFacade(pokerCasinoMock.Object, new IDealer());
        var player1 = new Player(new PlayerAccount(100));
        var player2 = new Player(new PlayerAccount(100));
        var bettingStrategyMock1 = new Mock<IBettingStrategy>();
        var bettingStrategyMock2 = new Mock<IBettingStrategy>();
        bettingStrategyMock1.Setup(strategy => strategy.GetBet()).Returns(10);
        bettingStrategyMock2.Setup(strategy => strategy.GetBet()).Returns(20);
        facade.AddPlayer(player1, bettingStrategyMock1.Object);
        facade.AddPlayer(player2, bettingStrategyMock2.Object);

        // Act
        facade.CollectBets();

        // Assert
        Assert.Equal(10, player1.CurrentBet);
        Assert.Equal(20, player2.CurrentBet);
    }

    [Fact]
    public void ReplacePlayerCards_ShouldReplaceCardsInThePlayerHand()
    {
        var pokerCasinoMock = new Mock<IPokerCasino>();
        var dealerMock = new Mock<IDealer>();
        var facade = new PokerGameFacade(pokerCasinoMock.Object, dealerMock.Object);

        var player = new Player(new PlayerAccount(100));
        facade.AddPlayer(player, Mock.Of<IBettingStrategy>());

        dealerMock.Setup(dealer => dealer.DealCards(1))
            .Returns(() => new List<Card> { new(Suit.Hearts, Rank.Ace) });

        facade.DealCards();

        var initialCardCount = player.Hand.Cards.Count;

        facade.ReplacePlayerCards(player, new List<int> { 0, 1, 2 });

        Assert.Equal(initialCardCount, player.Hand.Cards.Count);
        Assert.All(player.Hand.Cards, card =>
        {
            Assert.Equal(Suit.Hearts, card.Suit);
            Assert.Equal(Rank.Ace, card.Rank);
        });
    }

    [Fact]
    public void DetermineWinnerAndSettleAccounts_ShouldPayWinningPlayerAndChargeLosingPlayers()
    {
        var pokerCasinoMock = new Mock<IPokerCasino>();
        var facade = new PokerGameFacade(pokerCasinoMock.Object, new IDealer());
        var player1 = new Player(new PlayerAccount(100));
        var player2 = new Player(new PlayerAccount(100));
        facade.AddPlayer(player1, Mock.Of<IBettingStrategy>());
        facade.AddPlayer(player2, Mock.Of<IBettingStrategy>());

        player1.Hand = new Hand(new List<Card>
        {
            new(Suit.Hearts, Rank.Ace),
            new(Suit.Hearts, Rank.King),
            new(Suit.Hearts, Rank.Queen),
            new(Suit.Hearts, Rank.Jack),
            new(Suit.Hearts, Rank.Ten),
        });
        player2.Hand = new Hand(new List<Card>
        {
            new(Suit.Spades, Rank.Ace),
            new(Suit.Spades, Rank.King),
            new(Suit.Spades, Rank.Queen),
            new(Suit.Spades, Rank.Jack),
            new(Suit.Spades, Rank.Ten),
        });

        facade.DetermineWinnerAndSettleAccounts();

        pokerCasinoMock.Verify(casino => casino.PayWinning(player1, It.IsAny<decimal>()), Times.Once);
        pokerCasinoMock.Verify(casino => casino.ChargeLoss(player2, It.IsAny<decimal>()), Times.Once);
    }
}
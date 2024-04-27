using Moq;
using casino.Models;
using casino.Interfaces;
using Xunit;

namespace casino.Tests;

public class BJTests
{
    [Fact]
    public void AddPlayer_ShouldAddPlayerToList()
    {
        var facade = new BlackjackGameFacade(Mock.Of<IBank>());
        var player = new Player(Mock.Of<IPlayerAccount>());

        facade.AddPlayer(player);

        Assert.Contains(player, facade.Players);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    public void PlaceBets_ShouldPlaceBetsForAllPlayers(decimal betAmount)
    {
        var facade = new BlackjackGameFacade(Mock.Of<IBank>());
        var players = new[]
        {
            new Player(new PlayerAccount(100)),
            new Player(new PlayerAccount(100)),
            new Player(new PlayerAccount(100))
        };

        foreach (var player in players)
        {
            facade.AddPlayer(player);
        }

        facade.PlaceBets(betAmount);

        Assert.All(players, player => Assert.Equal(betAmount, player.CurrentBet));
    }

    [Fact]
    public void DealInitialCards_ShouldDealCorrectNumberOfCards()
    {
        var facade = new BlackjackGameFacade(Mock.Of<IBank>());
        var player = new Player(Mock.Of<IPlayerAccount>());
        facade.AddPlayer(player);

        facade.DealInitialCards();

        Assert.Equal(2, player.Hand.Cards.Count);
        Assert.Single(facade.DealersHand.Cards);
    }

    [Fact]
    public void PlayRound_ShouldCorrectlyDetermineWinnersAndLosers()
    {
        var bankMock = new Mock<IBank>();
        var facade = new BlackjackGameFacade(bankMock.Object);
        var player1 = new Player(Mock.Of<IPlayerAccount>());
        var player2 = new Player(Mock.Of<IPlayerAccount>());
        facade.AddPlayer(player1);
        facade.AddPlayer(player2);
        
        var playerTurnStrategy = new Mock<ITurnStrategy>();
        playerTurnStrategy.Setup(strategy => strategy.PerformTurn(It.IsAny<Hand>()))
            .Callback<Hand>(hand => hand.AddCard(new Card(Suit.Hearts, Rank.Ten)));

        var dealerTurnStrategy = new Mock<ITurnStrategy>();
        dealerTurnStrategy.Setup(strategy => strategy.PerformTurn(It.IsAny<Hand>()))
            .Callback<Hand>(hand => hand.AddCard(new Card(Suit.Diamonds, Rank.Jack)));

        facade.PlayRound(playerTurnStrategy.Object, dealerTurnStrategy.Object);
        
        bankMock.Verify(bank => bank.Withdraw(player1.Account, It.IsAny<decimal>()), Times.Once);
        bankMock.Verify(bank => bank.Withdraw(player2.Account, It.IsAny<decimal>()), Times.Once);
    }
}
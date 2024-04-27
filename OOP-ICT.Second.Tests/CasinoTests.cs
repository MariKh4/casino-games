using casino.Models;
using Xunit;

namespace casino.Tests;

public class CasinoTests
{
    [Fact]
    public void PayWinning_ShouldDepositAmountToPlayerAccount()
    {
        var bank = new Bank();
        var casino = new BlackjackCasino(bank);
        var playerAccount = new PlayerAccount(1100);
        var player = new Player(playerAccount);

        casino.PayWinning(player, 600);

        Assert.Equal(1700, player.Account.Balance);
    }

    [Fact]
    public void ChargeLoss_ShouldWithdrawAmountFromPlayerAccount()
    {
        var bank = new Bank();
        var casino = new BlackjackCasino(bank);
        var playerAccount = new PlayerAccount(8000);
        var player = new Player(playerAccount);

        casino.ChargeLoss(player, 200);

        Assert.Equal(7800, player.Account.Balance);
    }

    [Fact]
    public void ProcessBlackjack_ShouldPayWinningForBlackjack()
    {
        var bank = new Bank();
        var casino = new BlackjackCasino(bank);
        var playerAccount = new PlayerAccount(1000);
        var player = new Player(playerAccount);
        player.PlaceBet(1000);

        player.Hand.AddCard(new Card(Suit.Hearts, Rank.Ace));
        player.Hand.AddCard(new Card(Suit.Diamonds, Rank.Ten));

        casino.ProcessBlackjack(player);

        Assert.Equal(2000, player.Account.Balance); 
    }
}
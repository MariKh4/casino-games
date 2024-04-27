using casino.Models;
using Xunit;

namespace casino.Tests;

public class BankTests
{
    [Fact]
    public void Deposit_ShouldIncreasePlayerAccountBalance()
    {
        var bank = new Bank();
        var playerAccount = new PlayerAccount(1000);

        bank.Deposit(playerAccount, 200);

        Assert.Equal(1200, playerAccount.Balance);
    }

    [Fact]
    public void Withdraw_ShouldDecreasePlayerAccountBalance()
    {
        var bank = new Bank();
        var playerAccount = new PlayerAccount(1000);

        var result = bank.Withdraw(playerAccount, 200);

        Assert.True(result);
        Assert.Equal(800, playerAccount.Balance);
    }

    [Fact]
    public void Withdraw_InsufficientFunds_ShouldReturnFalse()
    {
        var bank = new Bank();
        var playerAccount = new PlayerAccount(100);

        var result = bank.Withdraw(playerAccount, 200);

        Assert.False(result);
        Assert.Equal(100, playerAccount.Balance);
    }

    [Fact]
    public void HasSufficientFunds_EnoughBalance_ShouldReturnTrue()
    {
        var bank = new Bank();
        var playerAccount = new PlayerAccount(300);

        var result = bank.HasSufficientFunds(playerAccount, 200);

        Assert.True(result);
    }

    [Fact]
    public void HasSufficientFunds_InsufficientBalance_ShouldReturnFalse()
    {
        var bank = new Bank();
        var playerAccount = new PlayerAccount(100);

        var result = bank.HasSufficientFunds(playerAccount, 200);

        Assert.False(result);
    }
}
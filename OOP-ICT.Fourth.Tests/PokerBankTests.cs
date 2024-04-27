using Moq;
using casino.PokerModels;
using casino.Interfaces;
using Xunit;

namespace casino.Tests;

public class PokerBankTests
    {
        [Fact]
        public void Deposit_ShouldIncreaseBalanceAndCallPlayerAccountDeposit()
        {
            var initialBalance = 100m;
            var playerAccountMock = new Mock<IPlayerAccount>();
            var pokerBank = new PokerBank(initialBalance);

            pokerBank.Deposit(playerAccountMock.Object, 50m);

            Assert.Equal(initialBalance + 50m, pokerBank.Balance);
            playerAccountMock.Verify(account => account.Deposit(50m), Times.Once);
        }

        [Fact]
        public void Withdraw_WithSufficientFunds_ShouldDecreaseBalanceAndCallPlayerAccountWithdraw()
        {
            var initialBalance = 100m;
            var playerAccountMock = new Mock<IPlayerAccount>();
            playerAccountMock.Setup(account => account.Balance).Returns(60m);
            var pokerBank = new PokerBank(initialBalance);

            var result = pokerBank.Withdraw(playerAccountMock.Object, 50m);

            Assert.True(result);
            Assert.Equal(initialBalance - 50m, pokerBank.Balance);
            playerAccountMock.Verify(account => account.Withdraw(50m), Times.Once);
        }

        [Fact]
        public void Withdraw_WithInsufficientFunds_ShouldNotChangeBalanceAndNotCallPlayerAccountWithdraw()
        {
            var initialBalance = 100m;
            var playerAccountMock = new Mock<IPlayerAccount>();
            playerAccountMock.Setup(account => account.Balance).Returns(40m);
            var pokerBank = new PokerBank(initialBalance);

            var result = pokerBank.Withdraw(playerAccountMock.Object, 50m);

            Assert.False(result);
            Assert.Equal(initialBalance, pokerBank.Balance);
            playerAccountMock.Verify(account => account.Withdraw(It.IsAny<decimal>()), Times.Never);
        }
    }
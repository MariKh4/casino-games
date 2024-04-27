using casino.Models;

namespace casino.Interfaces;

public interface IBlackjackCasino
{
    void PayWinning(Player player, decimal amount);
    void ChargeLoss(Player player, decimal amount);
    void ProcessBlackjack(Player player);
}
using casino.Models;

namespace casino.PokerInterfaces;

public interface IPokerCasino
{
    public void PayWinning(Player player, decimal amount);
    public void ChargeLoss(Player player, decimal amount);
}
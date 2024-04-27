using casino.Models;

namespace casino.Interfaces;

public interface ITurnStrategy
{
    void PerformTurn(Hand hand);

}
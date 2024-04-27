using casino.Models;

namespace casino.PokerModels;

 public class PokerHandEvaluator
    {
        public enum PokerHand
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            Straight,
            Flush,
            FullHouse,
            FourOfAKind,
            StraightFlush,
            RoyalFlush
        }

        public PokerHand EvaluateHand(Hand hand)
        {
            if (IsRoyalFlush(hand))
            {
                return PokerHand.RoyalFlush;
            }

            if (IsStraightFlush(hand))
            {
                return PokerHand.StraightFlush;
            }

            if (IsFourOfAKind(hand))
            {
                return PokerHand.FourOfAKind;
            }

            if (IsFullHouse(hand))
            {
                return PokerHand.FullHouse;
            }

            if (IsFlush(hand))
            {
                return PokerHand.Flush;
            }

            if (IsStraight(hand))
            {
                return PokerHand.Straight;
            }

            if (IsThreeOfAKind(hand))
            {
                return PokerHand.ThreeOfAKind;
            }

            if (IsTwoPair(hand))
            {
                return PokerHand.TwoPair;
            }

            if (IsOnePair(hand))
            {
                return PokerHand.OnePair;
            }

            return PokerHand.HighCard;
        }

        private bool IsRoyalFlush(Hand hand)
        {

            return IsStraight(hand) && IsFlush(hand) && hand.Cards.All(card => card.Rank >= Rank.Ten);
        }

        private bool IsStraightFlush(Hand hand)
        {
            return IsStraight(hand) && IsFlush(hand);
        }

        private bool IsFourOfAKind(Hand hand)
        {
            return hand.Cards.GroupBy(card => card.Rank).Any(group => group.Count() == 4);
        }

        private bool IsFullHouse(Hand hand)
        {
            var groupedCards = hand.Cards.GroupBy(card => card.Rank).ToList();
            return groupedCards.Count == 2 && (groupedCards[0].Count() == 3 || groupedCards[0].Count() == 2);
        }

        private bool IsFlush(Hand hand)
        {
            return hand.Cards.GroupBy(card => card.Suit).Any(group => group.Count() == 5);
        }

        private bool IsStraight(Hand hand)
        {
            var ranks = hand.Cards.OrderBy(card => card.Rank).Select(card => (int)card.Rank).Distinct().ToList();

            return ranks.Count switch
            {
                5 => ranks.Max() - ranks.Min() == 4,
                4 when ranks.Contains((int)Rank.Ace) => ranks.Max() - ranks.Min() == 3,
                _ => false
            };
        }

        private bool IsThreeOfAKind(Hand hand)
        {
            return hand.Cards.GroupBy(card => card.Rank).Any(group => group.Count() == 3);
        }

        private bool IsTwoPair(Hand hand)
        {
            var groupedCards = hand.Cards.GroupBy(card => card.Rank).ToList();
            return groupedCards.Count == 3 && groupedCards.All(group => group.Count() == 2);
        }

        private bool IsOnePair(Hand hand)
        {
            return hand.Cards.GroupBy(card => card.Rank).Any(group => group.Count() == 2);
        }
    }
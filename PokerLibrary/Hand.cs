using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    public class Hand
    {
        private List<Card> _cards;

        public Hand(List<Card> cards)
        {
            _cards = cards;
        }

        public string HandValue()
        {
            TestValidHand();

            if (TestRoyalStraightFlush())
                return ("Royal straight flush!");

            if (TestStraightFlush())
                return string.Format("Straight flush, {0} high", GetStraightHighestCard());

            if (TestFourOfAKind())
                return string.Format("Four of a kind, {0}", GetGroupedCard());

            if (TestFull())
                return string.Format("Full, {0}", GetFullMembers());

            if (TestFlush())
                return string.Format("Flush, {0} high", GetHighestCard());

            if (TestStraight())
                return string.Format("Straight, {0} high", GetStraightHighestCard());

            if (TestThreeOfAKind())
                return string.Format("Three of a kind, {0}", GetGroupedCard());

            if (TestTwoPair())
                return string.Format("Two pair, {0}", GetTwoPairCards());

            if (TestPair())
                return string.Format("Pair, {0}", GetGroupedCard());
            
            return string.Format("High card {0}", GetHighestCard());
        }

        private bool TestRoyalStraightFlush()
        {
            return TestStraightFlush() && GetStraightHighestCard().Equals(PokerNumber.A);
        }

        private bool TestStraightFlush()
        {
            return (AreSameSuit() && AreConsecutive());
        }

        private bool TestFourOfAKind()
        {
            return _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).Max(y => y.size) == 4;
        }

        private bool TestFull()
        {
            var groups = _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() });

            return groups.Max(x => x.size) == 3 && groups.Min(x => x.size) == 2;
        }

        private bool TestFlush()
        {
            return AreSameSuit();
        }

        private bool TestStraight()
        {
            return AreConsecutive();
        }

        private bool TestThreeOfAKind()
        {
            return _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).Max(y => y.size) == 3;
        }

        private bool TestTwoPair()
        {
            return _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).Count(y => y.size == 2) == 2;
        }

        private bool TestPair()
        {
            return _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).Max(y => y.size) == 2;
        }

        private void TestValidHand()
        {
            if (_cards == null || _cards.Count == 0)
                throw new ArgumentException("Hand is empty.");

            if (_cards.Count < 5)
                throw new ArgumentException("Hand contains less than 5 cards.");

            if (_cards.Count > 5)
                throw new ArgumentException("Hand contains more than 5 cards.");

            if (_cards.Select(card => _cards.Count(x => Equals(x.Number, card.Number) && Equals(x.Suit, card.Suit))).Any(countOfCards => countOfCards > 1))
            {
                throw new ArgumentException("There are more than 1 equal card in the hand");
            } 
        }

        private bool AreSameSuit()
        {
            return _cards.Where(c => c.Suit.Equals(_cards.First().Suit)).Count() == _cards.Count();
        }

        private bool AreConsecutive()
        {
            _cards = _cards.OrderBy(c => c.Number).ToList();

            var previous = 0;
            foreach (var c in _cards)
            {
                if (previous == 0 ||(int)c.Number == previous + 1)
                    previous = (int)c.Number;
                else
                    return false;
            }

            return true;
        }

        private PokerNumber GetHighestCard()
        {
            if (_cards.Any(c => c.Number == PokerNumber.A)) return PokerNumber.A;
            
            return _cards.OrderBy(c => c.Number).Max(c => c.Number);
        }

        private PokerNumber GetStraightHighestCard()
        {
            if (_cards.Any(c => c.Number == PokerNumber.A) && _cards.Any(c => c.Number == PokerNumber.K)) return PokerNumber.A;
            return _cards.OrderBy(c => c.Number).Max(c => c.Number);
        }

        private string GetGroupedCard()
        {
            return _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).OrderByDescending(x => x.size).First().key.ToString();
        }

        private string GetFullMembers()
        {
            var trio = _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).OrderByDescending(x => x.size).First().key.ToString();
            var pair = _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).OrderByDescending(x => x.size).Last().key.ToString();

            return string.Format("{0} and {1}", trio, pair);
        }

        private string GetTwoPairCards()
        {
            var lowestPair = _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).OrderByDescending(x => x.size).ElementAt(0).key;
            var highestPair = _cards.GroupBy(c => c.Number).Select(x => new { key = x.Key, size = x.Count() }).OrderByDescending(x => x.size).ElementAt(1).key;

            //Ace will always be in the "lowestPair"
            if (lowestPair.Equals(PokerNumber.A))
                return string.Format("{0} and {1}", lowestPair.ToString(), highestPair.ToString());

            return string.Format("{0} and {1}", highestPair.ToString(), lowestPair.ToString());
        }
    }
}

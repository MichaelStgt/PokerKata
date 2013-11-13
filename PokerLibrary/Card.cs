using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerLibrary
{
    public enum Suit { Clubs, Spades, Hearts, Diamonds }
    public enum PokerNumber { A, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, J, Q, K }

    public class Card
    {
        private PokerNumber _number;
        private Suit _suit;

        public Card(PokerNumber number, Suit suit)
        {
            _number = number;
            _suit = suit;
        }

        public PokerNumber Number { get { return _number; } }
        public Suit Suit { get { return _suit;} }
    }
}

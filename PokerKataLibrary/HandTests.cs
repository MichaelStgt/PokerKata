using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerLibrary;

namespace PokerKataLibrary
{
    [TestClass]
    public class HandTests
    {
        #region Errors
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPokerHand_EmptyHand()
        {
            var hand = new Hand(new List<Card>());

            var handValue = hand.HandValue();

            Assert.Fail("This point should not be reached");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPokerHand_LessThanFiveCards()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Five, Suit.Diamonds),
                                    new Card(PokerNumber.Ten, Suit.Spades),
                                    new Card(PokerNumber.J, Suit.Hearts),
                                    new Card(PokerNumber.K, Suit.Clubs)
                                }
                      );

            var handValue = hand.HandValue();

            Assert.Fail("This point should not be reached");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPokerHand_MoreThanFiveCards()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Five, Suit.Diamonds),
                                    new Card(PokerNumber.Q, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Spades),
                                    new Card(PokerNumber.Ten, Suit.Spades),
                                    new Card(PokerNumber.J, Suit.Hearts),
                                    new Card(PokerNumber.K, Suit.Clubs)
                                }
                      );

            var handValue = hand.HandValue();

            Assert.Fail("This point should not be reached");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPokerHand_RepeatedCard()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Five, Suit.Diamonds),
                                    new Card(PokerNumber.Five, Suit.Diamonds),
                                    new Card(PokerNumber.Ten, Suit.Spades),
                                    new Card(PokerNumber.J, Suit.Hearts),
                                    new Card(PokerNumber.K, Suit.Clubs)
                                }
                       );
            
            var handValue = hand.HandValue();

            Assert.Fail("This point should not be reached");
        }
        #endregion

        #region standard tests

        [TestMethod]
        public void TestPokerHand_StraightFlush()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Five, Suit.Diamonds),
                                    new Card(PokerNumber.Six, Suit.Diamonds),
                                    new Card(PokerNumber.Seven, Suit.Diamonds),
                                    new Card(PokerNumber.Eight, Suit.Diamonds),
                                    new Card(PokerNumber.Nine, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Straight flush, Nine high", handValue);
        }

        [TestMethod]
        public void TestPokerHand_LowestStraightFlush()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.A, Suit.Diamonds),
                                    new Card(PokerNumber.Two, Suit.Diamonds),
                                    new Card(PokerNumber.Three, Suit.Diamonds),
                                    new Card(PokerNumber.Four, Suit.Diamonds),
                                    new Card(PokerNumber.Five, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Straight flush, Five high", handValue);
        }

        [TestMethod]
        public void TestPokerHand_RoyalStraightFlush()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Ten, Suit.Diamonds),
                                    new Card(PokerNumber.J, Suit.Diamonds),
                                    new Card(PokerNumber.Q, Suit.Diamonds),
                                    new Card(PokerNumber.K, Suit.Diamonds),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Royal straight flush!", handValue);
        }

        [TestMethod]
        public void TestPokerHand_FourOfAKind()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Ten, Suit.Diamonds),
                                    new Card(PokerNumber.Ten, Suit.Clubs),
                                    new Card(PokerNumber.Ten, Suit.Spades),
                                    new Card(PokerNumber.Ten, Suit.Hearts),
                                    new Card(PokerNumber.Six, Suit.Clubs)
                                }
                          );
            var handValue = hand.HandValue();

            Assert.AreEqual("Four of a kind, Ten", handValue);

        }

        [TestMethod]
        public void TestPokerHand_Full()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Three, Suit.Spades),                    
                                    new Card(PokerNumber.Ten, Suit.Diamonds),
                                    new Card(PokerNumber.Ten, Suit.Clubs),
                                    new Card(PokerNumber.Three, Suit.Hearts),
                                    new Card(PokerNumber.Three, Suit.Clubs)
                                }
                          );
            var handValue = hand.HandValue();

            Assert.AreEqual("Full, Three and Ten", handValue);

        }

        [TestMethod]
        public void TestPokerHand_Flush()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Ten, Suit.Hearts),
                                    new Card(PokerNumber.Eight, Suit.Hearts),
                                    new Card(PokerNumber.A, Suit.Hearts),
                                    new Card(PokerNumber.Two, Suit.Hearts),
                                    new Card(PokerNumber.Six, Suit.Hearts)
                                }
                          );
            var handValue = hand.HandValue();

            Assert.AreEqual("Flush, A high", handValue);

        }

        [TestMethod]
        public void TestPokerHand_Straight()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Ten, Suit.Diamonds),
                                    new Card(PokerNumber.Q, Suit.Diamonds),
                                    new Card(PokerNumber.Nine, Suit.Clubs),
                                    new Card(PokerNumber.J, Suit.Diamonds),
                                    new Card(PokerNumber.K, Suit.Spades)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Straight, K high", handValue);
        }

        [TestMethod]
        public void TestPokerHand_LowestStraight()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.A, Suit.Spades),
                                    new Card(PokerNumber.Two, Suit.Diamonds),
                                    new Card(PokerNumber.Three, Suit.Hearts),
                                    new Card(PokerNumber.Four, Suit.Hearts),
                                    new Card(PokerNumber.Five, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Straight, Five high", handValue);
        }

        [TestMethod]
        public void TestPokerHand_HighestStraight()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Ten, Suit.Diamonds),
                                    new Card(PokerNumber.J, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Clubs),
                                    new Card(PokerNumber.K, Suit.Clubs),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Straight, A high", handValue);
        }

        [TestMethod]
        public void TestPokerHand_ThreeOfAKind()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Q, Suit.Diamonds),
                                    new Card(PokerNumber.J, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Hearts),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Three of a kind, Q", handValue);
        }

        [TestMethod]
        public void TestPokerHand_TwoPair()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Q, Suit.Diamonds),
                                    new Card(PokerNumber.J, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Clubs),
                                    new Card(PokerNumber.J, Suit.Hearts),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Two pair, Q and J", handValue);
        }

        [TestMethod]
        public void TestPokerHand_Pair()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Q, Suit.Diamonds),
                                    new Card(PokerNumber.J, Suit.Clubs),
                                    new Card(PokerNumber.Q, Suit.Clubs),
                                    new Card(PokerNumber.Six, Suit.Hearts),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Pair, Q", handValue);
        }

        [TestMethod]
        public void TestPokerHand_HighCardTen()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Eight, Suit.Diamonds),
                                    new Card(PokerNumber.Seven, Suit.Clubs),
                                    new Card(PokerNumber.Two, Suit.Clubs),
                                    new Card(PokerNumber.Six, Suit.Hearts),
                                    new Card(PokerNumber.Ten, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("High card Ten", handValue);
        }

        [TestMethod]
        public void TestPokerHand_HighCardAce()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Eight, Suit.Diamonds),
                                    new Card(PokerNumber.A, Suit.Clubs),
                                    new Card(PokerNumber.Two, Suit.Clubs),
                                    new Card(PokerNumber.Six, Suit.Hearts),
                                    new Card(PokerNumber.Ten, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("High card A", handValue);
        }

        [TestMethod]
        public void TestPokerHand_AceIsAlwaysHighestCardInTwoPair()
        {
            var hand = new Hand(new List<Card> {
                                    new Card(PokerNumber.Two, Suit.Diamonds),
                                    new Card(PokerNumber.A, Suit.Clubs),
                                    new Card(PokerNumber.Two, Suit.Clubs),
                                    new Card(PokerNumber.Six, Suit.Hearts),
                                    new Card(PokerNumber.A, Suit.Diamonds)
                                }
                       );

            var handValue = hand.HandValue();

            Assert.AreEqual("Two pair, A and Two", handValue);
        }
        #endregion
    }
}

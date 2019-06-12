using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public class Card
    {
        public enum CardRank { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

        public enum CardSuit { Diamonds, Hearts, Spades, Clubs }

        private static readonly int _maxRankValue;
        private static readonly int _minRankValue;

        private static readonly int _maxSuitValue;
        private static readonly int _minSuitValue;

        private static Random random;

        public CardRank Rank;
        public CardSuit Suit;

        private int cost;

        static Card()
        {
            _maxRankValue = Enum.GetValues(typeof(CardRank)).Cast<int>().Max();
            _minRankValue = Enum.GetValues(typeof(CardRank)).Cast<int>().Min();

            _maxSuitValue = Enum.GetValues(typeof(CardSuit)).Cast<int>().Max();
            _minSuitValue = Enum.GetValues(typeof(CardSuit)).Cast<int>().Min();

            random = new Random();
        }

        public Card(CardRank rank, CardSuit suit)
        {
            this.Rank = rank;
            this.Suit = suit;

            DefineCost();
        }

        public static Card GetRandomCard()
        {

            CardRank randRank = (CardRank)random.Next(_minRankValue, _maxRankValue + 1);
            CardSuit randSuit = (CardSuit)random.Next(_minSuitValue, _maxSuitValue + 1);

            if (randRank == CardRank.Ace)
                return new Ace(randRank, randSuit);

            return new Card(randRank, randSuit);
        }

        public int GetCost()
        {
            return this.cost;
        }

        public void DefineCost()
        {
            switch (Rank)
            {
                case CardRank.Two:
                    this.cost = 2;
                    break;
                case CardRank.Three:
                    this.cost = 3;
                    break;
                case CardRank.Four:
                    this.cost = 4;
                    break;
                case CardRank.Five:
                    this.cost = 5;
                    break;
                case CardRank.Six:
                    this.cost = 6;
                    break;
                case CardRank.Seven:
                    this.cost = 7;
                    break;
                case CardRank.Eight:
                    this.cost = 8;
                    break;
                case CardRank.Nine:
                    this.cost = 9;
                    break;
                case CardRank.Ten:
                    this.cost = 10;
                    break;
                case CardRank.Jack:
                    this.cost = 10;
                    break;
                case CardRank.Queen:
                    this.cost = 10;
                    break;
                case CardRank.King:
                    this.cost = 10;
                    break;
                case CardRank.Ace:
                    this.cost = 11;
                    break;
                default:
                    this.cost = 0;
                    break;
            }
        }

        public override string ToString()
        {
            return $"{Enum.GetName(typeof(CardRank), this.Rank)} {Enum.GetName(typeof(CardSuit), this.Suit)} (Cost: {this.cost})";
        }
    }
}

using System;
using BlackJack_BSL.Interfaces;
using Common.Enums;

namespace BlackJack_BSL.Models
{
    public class Card:ICard
    {
        public CardRanks.CardRank Rank { get; set; }
        public CardSuits.CardSuit Suit { get; set; }
        public int Cost { get { return this.cost; } }

        private int cost;

        public Card(CardRanks.CardRank rank, CardSuits.CardSuit suit)
        {
            this.Rank = rank;
            this.Suit = suit;

            DefineCost();
        }
        public void DefineCost()
        {
            if (Rank == CardRanks.CardRank.Two)
            {
                this.cost = 2;
            }
            else if (Rank == CardRanks.CardRank.Three)
            {
                this.cost = 3;
            }
            else if (Rank == CardRanks.CardRank.Four)
            {
                this.cost = 4;
            }
            else if (Rank == CardRanks.CardRank.Five)
            {
                this.cost = 5;
            }
            else if (Rank == CardRanks.CardRank.Six)
            {
                this.cost = 6;
            }
            else if (Rank == CardRanks.CardRank.Seven)
            {
                this.cost = 7;
            }
            else if (Rank == CardRanks.CardRank.Eight)
            {
                this.cost = 8;
            }
            else if (Rank == CardRanks.CardRank.Nine)
            {
                this.cost = 9;
            }
            else if (Rank == CardRanks.CardRank.Ten)
            {
                this.cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Jack)
            {
                this.cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Queen)
            {
                this.cost = 10;
            }
            else if (Rank == CardRanks.CardRank.King)
            {
                this.cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Ace)
            {
                this.cost = 11;
            }
            else
            {
                this.cost = 0;
            }
        }
        
    }
}

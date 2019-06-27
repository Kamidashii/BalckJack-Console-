using System;
using BlackJackBusinessLogic.Interfaces;
using Common.Enums;

namespace BlackJackBusinessLogic.Models
{
    public class Card: Interfaces.Models.ICard
    {
        public CardRanks.CardRank Rank { get; set; }
        public CardSuits.CardSuit Suit { get; set; }
        public int Cost { get; private set; }

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
                this.Cost = 2;
            }
            else if (Rank == CardRanks.CardRank.Three)
            {
                this.Cost = 3;
            }
            else if (Rank == CardRanks.CardRank.Four)
            {
                this.Cost = 4;
            }
            else if (Rank == CardRanks.CardRank.Five)
            {
                this.Cost = 5;
            }
            else if (Rank == CardRanks.CardRank.Six)
            {
                this.Cost = 6;
            }
            else if (Rank == CardRanks.CardRank.Seven)
            {
                this.Cost = 7;
            }
            else if (Rank == CardRanks.CardRank.Eight)
            {
                this.Cost = 8;
            }
            else if (Rank == CardRanks.CardRank.Nine)
            {
                this.Cost = 9;
            }
            else if (Rank == CardRanks.CardRank.Ten)
            {
                this.Cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Jack)
            {
                this.Cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Queen)
            {
                this.Cost = 10;
            }
            else if (Rank == CardRanks.CardRank.King)
            {
                this.Cost = 10;
            }
            else if (Rank == CardRanks.CardRank.Ace)
            {
                this.Cost = 11;
            }
        }
        
    }
}

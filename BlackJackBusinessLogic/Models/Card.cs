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
            Rank = rank;
            Suit = suit;

            DefineCost();
        }
        public void DefineCost()
        {
            if (Rank == CardRanks.CardRank.Two)
            {
                Cost = 2;
            }
            if (Rank == CardRanks.CardRank.Three)
            {
                Cost = 3;
            }
            if (Rank == CardRanks.CardRank.Four)
            {
                Cost = 4;
            }
            if (Rank == CardRanks.CardRank.Five)
            {
                Cost = 5;
            }
            if (Rank == CardRanks.CardRank.Six)
            {
                Cost = 6;
            }
            if (Rank == CardRanks.CardRank.Seven)
            {
                Cost = 7;
            }
            if (Rank == CardRanks.CardRank.Eight)
            {
                Cost = 8;
            }
            if (Rank == CardRanks.CardRank.Nine)
            {
                Cost = 9;
            }
            if (Rank == CardRanks.CardRank.Ten)
            {
                Cost = 10;
            }
            if (Rank == CardRanks.CardRank.Jack)
            {
                Cost = 10;
            }
            if (Rank == CardRanks.CardRank.Queen)
            {
                Cost = 10;
            }
            if (Rank == CardRanks.CardRank.King)
            {
                Cost = 10;
            }
            if (Rank == CardRanks.CardRank.Ace)
            {
                Cost = 11;
            }
        }
        
    }
}

using System;
using HelpfulValues.Enums;

namespace DA_Layer.Models
{
    public class Card
    {
        public Card_Enums.CardRank Rank;
        public Card_Enums.CardSuit Suit;

        private int cost;

        public Card(Card_Enums.CardRank rank, Card_Enums.CardSuit suit)
        {
            this.Rank = rank;
            this.Suit = suit;

            DefineCost();
        }

        public int GetCost()
        {
            return this.cost;
        }

        public void DefineCost()
        {
            if (Rank == Card_Enums.CardRank.Two)
            {
                this.cost = 2;
            }
            else if (Rank == Card_Enums.CardRank.Three)
            {
                this.cost = 3;
            }
            else if (Rank == Card_Enums.CardRank.Four)
            {
                this.cost = 4;
            }
            else if (Rank == Card_Enums.CardRank.Five)
            {
                this.cost = 5;
            }
            else if (Rank == Card_Enums.CardRank.Six)
            {
                this.cost = 6;
            }
            else if (Rank == Card_Enums.CardRank.Seven)
            {
                this.cost = 7;
            }
            else if (Rank == Card_Enums.CardRank.Eight)
            {
                this.cost = 8;
            }
            else if (Rank == Card_Enums.CardRank.Nine)
            {
                this.cost = 9;
            }
            else if (Rank == Card_Enums.CardRank.Ten)
            {
                this.cost = 10;
            }
            else if (Rank == Card_Enums.CardRank.Jack)
            {
                this.cost = 10;
            }
            else if (Rank == Card_Enums.CardRank.Queen)
            {
                this.cost = 10;
            }
            else if (Rank == Card_Enums.CardRank.King)
            {
                this.cost = 10;
            }
            else if (Rank == Card_Enums.CardRank.Ace)
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

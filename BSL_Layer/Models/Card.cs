using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Interfaces;
using HelpfulValues.Enums;

namespace BSL_Layer.Models
{
    public class Card:ICard
    {
        public Card_Enums.CardRank Rank { get; set; }
        public Card_Enums.CardSuit Suit { get; set; }
        public int Cost { get { return this.cost; } }

        private int cost;

        public Card(Card_Enums.CardRank rank, Card_Enums.CardSuit suit)
        {
            this.Rank = rank;
            this.Suit = suit;

            DefineCost();
        }

        public Card(DA_Layer.Models.Card DAcard)
        {
            this.Rank = DAcard.Rank;
            this.Suit = DAcard.Suit;

            DefineCost();
        }

        public virtual DA_Layer.Models.Card GetDBCard()
        {
            DA_Layer.Models.Card card = new DA_Layer.Models.Card(this.Rank, this.Suit);
            card.DefineCost();
            return card;
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

using System;
using Common.Enums;

namespace BlackJack_DA.Models
{
    public class Ace : Card
    {
        public int SpecialCost = 1;
        public bool IsSpecialOn = false;

        public Ace(CardRanks.CardRank rank, CardSuits.CardSuit suit) : base(rank, suit) { }

        public int GetSpecialCostDifference()
        {
            return this.Cost - this.SpecialCost;
        }
    }
}

using System;
using HelpfulValues.Enums;

namespace DA_Layer.Models
{
    public class Ace : Card
    {
        public readonly int specialCost = 1;
        public bool IsSpecialOn = false;

        public Ace(Card_Enums.CardRank rank, Card_Enums.CardSuit suit) : base(rank, suit) { }

        public int GetSpecialCostDifference()
        {
            return this.GetCost() - this.specialCost;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    class Ace : Card
    {
        private readonly int specialCost=1;
        public bool IsSpecialOn=false;

        public Ace(Enums.Card_Enums.CardRank rank, Enums.Card_Enums.CardSuit suit) : base(rank, suit)
        {
        }

        public int GetSpecialCostDifference()
        {
            return this.GetCost() - this.specialCost;
        }
    }
}

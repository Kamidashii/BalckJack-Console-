using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Layer.Models;
using HelpfulValues.Enums;

namespace BSL_Layer.Models
{
    public class Ace : Card
    {
        private readonly int specialCost = 1;
        public bool IsSpecialOn = false;

        public Ace(Card_Enums.CardRank rank, Card_Enums.CardSuit suit) : base(rank, suit) { }

        public int GetSpecialCostDifference()
        {
            return this.GetCost() - this.specialCost;
        }

        public Ace(DA_Layer.Models.Ace DAace) : base(DAace)
        {
            this.IsSpecialOn = DAace.IsSpecialOn;
            this.specialCost = DAace.specialCost;
        }

        public override DA_Layer.Models.Card GetDBCard()
        {
            DA_Layer.Models.Ace card = new DA_Layer.Models.Ace(this.Rank, this.Suit);
            card.DefineCost();
            card.IsSpecialOn = this.IsSpecialOn;
            return card;
        }
    }
}

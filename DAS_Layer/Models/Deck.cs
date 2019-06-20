using System;
using System.Collections.Generic;
using System.Linq;
using HelpfulValues.Constants;

namespace DA_Layer.Models
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>(Deck_Constants.CARDS_COUNT);

        public Card TakeCard()
        {
            Card card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Constants;

namespace BlackJack_DA.Models
{
    public class Deck
    {
        public List<Card> Cards = new List<Card>(Deck_Constants.CardsCount);

        public Card TakeCard()
        {
            Card card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}

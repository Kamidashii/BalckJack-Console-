using System;
using System.Collections.Generic;
using System.Linq;
using BSL_Layer.Interfaces;
using HelpfulValues.Constants;

namespace BSL_Layer.Models
{
    public class Deck
    {
        public List<ICard> Cards = new List<ICard>(Deck_Constants.CARDS_COUNT);

        public ICard TakeCard()
        {
            ICard card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}

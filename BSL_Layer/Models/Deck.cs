using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_BSL.Interfaces;
using Common.Constants;

namespace BlackJack_BSL.Models
{
    public class Deck:IDeck
    {
        public List<ICard> Cards { get; set; } = new List<ICard>(Deck_Constants.CardsCount);

        public ICard TakeCard()
        {
            ICard card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}

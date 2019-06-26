using System;
using System.Collections.Generic;
using System.Linq;
using BlackJack_BSL.Interfaces;
using Common.Constants;

namespace BlackJack_BSL.Models
{
    public class Deck: Interfaces.Models.IDeck
    {
        public List<Interfaces.Models.ICard> Cards { get; set; } = new List<Interfaces.Models.ICard>(Deck_Constants.CardsCount);

        public Interfaces.Models.ICard TakeCard()
        {
            Interfaces.Models.ICard card = this.Cards.First();
            this.Cards.Remove(this.Cards.First());

            return card;
        }
    }
}

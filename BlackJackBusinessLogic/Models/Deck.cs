using System;
using System.Collections.Generic;
using System.Linq;
using BlackJackBusinessLogic.Interfaces;
using Common.Constants;

namespace BlackJackBusinessLogic.Models
{
    public class Deck: Interfaces.Models.IDeck
    {
        public List<Interfaces.Models.ICard> Cards { get; set; } = new List<Interfaces.Models.ICard>(Deck_Constants.CardsCount);

        public Interfaces.Models.ICard TakeCard()
        {
            Interfaces.Models.ICard firstCard = this.Cards.First();
            this.Cards.Remove(firstCard);

            return firstCard;
        }
    }
}

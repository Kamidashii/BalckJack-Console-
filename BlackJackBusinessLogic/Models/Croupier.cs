using System;
using BlackJackBusinessLogic.Interfaces;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Models
{
    public class Croupier : Player
    {
        public Croupier()
        {
            this.IsBot = true;
        }
        

        public Croupier(int score, List<Interfaces.Models.ICard> cards)
        {
            this.Score = score;
            this.Cards = cards;
            this.IsBot = true;
        }
    }
}

using System;
using BlackJack_BSL.Interfaces;
using System.Collections.Generic;

namespace BlackJack_BSL.Models
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

using System;
using BlackJackBusinessLogic.Interfaces;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Models
{
    public class Croupier : Player
    {
        public Croupier()
        {
            IsBot = true;
        }
        

        public Croupier(int score, List<Interfaces.Models.ICard> cards)
        {
            Score = score;
            Cards = cards;
            IsBot = true;
        }
    }
}

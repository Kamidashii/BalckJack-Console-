using BlackJackBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Models
{
    public abstract class Player: Interfaces.Models.IPlayer
    {
        public int Score { get; set; }
        public bool IsBot { get; set; }
        public List<Interfaces.Models.ICard> Cards { get; set; } = new List<Interfaces.Models.ICard>();
    }

   
}

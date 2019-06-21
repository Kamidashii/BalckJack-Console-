using BlackJack_BSL.Interfaces;
using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Models
{
    public abstract class Player:IPlayer
    {
        public int Score { get; set; } = 0;
        public bool IsBot { get; set; }
        public List<ICard> Cards { get; set; } = new List<ICard>();
    }

   
}

using System;
using System.Collections.Generic;

namespace DA_Layer.Models
{
    public abstract class Player
    {
        public int Score = 0;
        public bool IsBot;
        public List<Card> Cards = new List<Card>();
    }
}

using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DA_Layer.Models
{
    public class Croupier : Player
    {
        public Croupier()
        {
            this.IsBot = true;
        }

        [JsonConstructor]
        public Croupier(int score, List<Card> cards)
        {
            this.Score = score;
            this.Cards = cards;
            this.IsBot = true;
        }
    }
}

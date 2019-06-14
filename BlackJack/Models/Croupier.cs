using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
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

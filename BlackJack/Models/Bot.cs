using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
{
    public class Bot : User
    {
        public Enums.Bot_Enums.Bot_Demeanor Demeanor;


        public Bot(string name, int bet, Enums.Bot_Enums.Bot_Demeanor demeanor) : base(name, bet)
        {
            this.Demeanor = demeanor;
            this.IsBot = true;
        }

        [JsonConstructor]
        public Bot(string name, int bet, Enums.Bot_Enums.Bot_Demeanor demeanor, int score, List<Card> cards) : base(name, bet)
        {
            this.Score = score;
            this.Cards = cards;
            this.Demeanor = demeanor;
            this.IsBot = true;
        }
    }
}

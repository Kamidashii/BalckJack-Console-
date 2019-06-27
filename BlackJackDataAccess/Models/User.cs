using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlackJackDataAccess.Models
{
    public class User : Player
    {
        public int Bet;
        public string Name { get; set; }

        public User(string name, int bet)
        {
            this.Name = name;
            this.Bet = bet;
            this.IsBot = false;
        }
        
        [JsonConstructor]
        public User(string name,int bet,int score,List<Card>cards,bool isBot)
        {
            this.Name = name;
            this.Bet = bet;
            this.Score = score;
            this.Cards = cards;
            this.IsBot = isBot;
        }
    }
}

using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Common.Enums;

namespace BlackJackDataAccess.Models
{
    public class Bot : User
    {
        public BotDemeanors.BotDemeanor Demeanor;


        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor) : base(name, bet)
        {
            this.Demeanor = demeanor;
            this.IsBot = true;
        }

        [JsonConstructor]
        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor, int score, List<Card> cards,bool isBot) : base(name, bet)
        {
            this.Score = score;
            this.Cards = cards;
            this.Demeanor = demeanor;
            this.IsBot = isBot;
        }
    }
}

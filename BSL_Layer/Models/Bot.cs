using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces;
using Common.Enums;

namespace BlackJack_BSL.Models
{
    public class Bot : User
    {
        public BotDemeanors.BotDemeanor Demeanor;


        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor) : base(name, bet)
        {
            this.Demeanor = demeanor;
            this.IsBot = true;
        }

        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor, int score, List<ICard> cards,bool isBot) : base(name, bet)
        {
            this.Score = score;
            this.Cards = cards;
            this.Demeanor = demeanor;
            this.IsBot = isBot;
        }
    }
}

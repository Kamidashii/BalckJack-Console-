using System;
using System.Collections.Generic;
using BlackJackBusinessLogic.Interfaces;
using Common.Enums;

namespace BlackJackBusinessLogic.Models
{
    public class Bot : User, Interfaces.Models.IBot
    {
        public BotDemeanors.BotDemeanor Demeanor { get; set; }


        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor) : base(name, bet)
        {
            Demeanor = demeanor;
            IsBot = true;
        }

        public Bot(string name, int bet, BotDemeanors.BotDemeanor demeanor, int score, List<Interfaces.Models.ICard> cards,bool isBot) : base(name, bet)
        {
            Score = score;
            Cards = cards;
            Demeanor = demeanor;
            IsBot = isBot;
        }
    }
}

using System;
using System.Collections.Generic;
using BSL_Layer.Interfaces;
using HelpfulValues.Enums;

namespace BSL_Layer.Models
{
    public class Bot : User
    {
        public Bot_Enums.Bot_Demeanor Demeanor;


        public Bot(string name, int bet, Bot_Enums.Bot_Demeanor demeanor) : base(name, bet)
        {
            this.Demeanor = demeanor;
            this.IsBot = true;
        }

        public Bot(DA_Layer.Models.Bot DAbot) : base(DAbot)
        {
            this.Demeanor = DAbot.Demeanor;
        }

        public override DA_Layer.Models.User GetDBUser()
        {
            DA_Layer.Models.Bot DAbot = new DA_Layer.Models.Bot(this.Name, this.Bet, this.Demeanor, this.Score, this.ConvertCardsToDB());
            return DAbot;
        }


        public Bot(string name, int bet, Bot_Enums.Bot_Demeanor demeanor, int score, List<ICard> cards) : base(name, bet)
        {
            this.Score = score;
            this.Cards = cards;
            this.Demeanor = demeanor;
            this.IsBot = true;
        }
    }
}

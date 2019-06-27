using System;
using BlackJackBusinessLogic.Interfaces;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Models
{
    public class User : Player, Interfaces.Models.IUser
    {
        public int Bet { get; set; }
        public string Name { get; set; }

        public User(string name, int bet)
        {
            this.Name = name;
            this.Bet = bet;
            this.IsBot = false;
        }


        public User(string name, int bet, int score, List<Interfaces.Models.ICard> cards,bool isBot)
        {
            this.Name = name;
            this.Bet = bet;
            this.Score = score;
            this.Cards = cards;
            this.IsBot = isBot;
        }
    }
}

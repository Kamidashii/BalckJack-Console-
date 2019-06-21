using System;
using BlackJack_BSL.Interfaces;
using System.Collections.Generic;

namespace BlackJack_BSL.Models
{
    public class User : Player,IUser
    {
        public int Bet { get; set; }
        public string Name { get; set; }

        public User(string name, int bet)
        {
            this.Name = name;
            this.Bet = bet;
            this.IsBot = false;
        }


        public User(string name, int bet, int score, List<ICard> cards,bool isBot)
        {
            this.Name = name;
            this.Bet = bet;
            this.Score = score;
            this.Cards = cards;
            this.IsBot = isBot;
        }
    }
}

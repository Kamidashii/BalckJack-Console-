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
            Name = name;
            Bet = bet;
            IsBot = false;
        }


        public User(string name, int bet, int score, List<Interfaces.Models.ICard> cards,bool isBot)
        {
            Name = name;
            Bet = bet;
            Score = score;
            Cards = cards;
            IsBot = isBot;
        }
    }
}

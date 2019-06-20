using System;
using BSL_Layer.Interfaces;
using System.Collections.Generic;

namespace BSL_Layer.Models
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


        public User(string name, int bet, int score, List<ICard> cards)
        {
            this.Name = name;
            this.Bet = bet;
            this.Score = score;
            this.Cards = cards;
            this.IsBot = false;
        }

        public User(DA_Layer.Models.User DAuser)
        {
            this.Bet = DAuser.Bet;
            this.Cards=ConvertCardsFromDB(DAuser.Cards);
            this.IsBot = DAuser.IsBot;
            this.Score = DAuser.Score;
            this.Name = DAuser.Name;
        }

        public virtual DA_Layer.Models.User GetDBUser()
        {
            DA_Layer.Models.User user = new DA_Layer.Models.User(this.Name, this.Bet, this.Score, this.ConvertCardsToDB());
            return user;
        }
    }
}

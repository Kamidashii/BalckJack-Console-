using System;
using BSL_Layer.Interfaces;
using System.Collections.Generic;

namespace BSL_Layer.Models
{
    public class Croupier : Player
    {
        public Croupier()
        {
            this.IsBot = true;
        }
        

        public Croupier(int score, List<ICard> cards)
        {
            this.Score = score;
            this.Cards = cards;
            this.IsBot = true;
        }

        public Croupier(DA_Layer.Models.Croupier DAcroupier)
        {
            this.IsBot = DAcroupier.IsBot;
            this.Score = DAcroupier.Score;
            this.Cards=ConvertCardsFromDB(DAcroupier.Cards);
        }

        public DA_Layer.Models.Croupier GetDBCroupier()
        {
            DA_Layer.Models.Croupier user = new DA_Layer.Models.Croupier(this.Score, this.ConvertCardsToDB());
            return user;
        }
    }
}

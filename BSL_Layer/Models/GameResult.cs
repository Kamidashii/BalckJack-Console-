using System;
using System.Collections.Generic;

namespace BSL_Layer.Models
{
    public class GameResult
    {
        public int GameId;
        public int AllGamesCount;

        public List<User> Winners;
        public List<User> Losers;
        public List<User> Draws;

        public Croupier Croupier;


        public GameResult(int gameId)
        {
            this.GameId = gameId;

            this.Winners = new List<User>();
            this.Losers = new List<User>();
            this.Draws = new List<User>();
        }

        public GameResult(DA_Layer.Models.GameResult DAresult)
        {
            this.AllGamesCount = DAresult.AllGamesCount;
            this.GameId = DAresult.GameId;

            this.Croupier = new Croupier(DAresult.Croupier);

            this.Winners = ConvertWinnersFromDB(DAresult.Winners);
            this.Losers = ConvertLosersFromDB(DAresult.Losers);
            this.Draws = ConvertDrawFromDB(DAresult.Draw);
        }

        public DA_Layer.Models.GameResult ConvertToDB()
        {
            DA_Layer.Models.GameResult DBres = new DA_Layer.Models.GameResult(this.GameId);
            DBres.AllGamesCount = this.AllGamesCount;

            DBres.Winners = ConvertWinnersToDB(this.Winners);
            DBres.Losers = ConvertLosersToDB(this.Losers);
            DBres.Draw = ConvertDrawsToDB(this.Draws);

            DBres.Croupier = this.Croupier.GetDBCroupier();

            return DBres;
        }

        private List<User> ConvertLosersFromDB(List<DA_Layer.Models.User> DAlosers)
        {
            List<User> losers = new List<User>();

            for (int i = 0; i < DAlosers.Count; ++i)
            {
                losers.Add(new User(DAlosers[i]));
            }

            return losers;
        }

        private List<User> ConvertWinnersFromDB(List<DA_Layer.Models.User> DAwinners)
        {
            List<User> winnders = new List<User>();

            for (int i = 0; i < DAwinners.Count; ++i)
            {
                winnders.Add(new User(DAwinners[i]));
            }

            return winnders;
        }

        private List<User> ConvertDrawFromDB(List<DA_Layer.Models.User> DAdraw)
        {
            List<User> draw = new List<User>();

            for (int i = 0; i < DAdraw.Count; ++i)
            {
                draw.Add(new User(DAdraw[i]));
            }

            return draw;
        }



        private List<DA_Layer.Models.User> ConvertLosersToDB(List<User> BSLlosers)
        {
            List<DA_Layer.Models.User> losers = new List<DA_Layer.Models.User>();

            for (int i = 0; i < BSLlosers.Count; ++i)
            {
                losers.Add(BSLlosers[i].GetDBUser());
            }

            return losers;
        }

        private List<DA_Layer.Models.User> ConvertWinnersToDB(List<User> DAwinners)
        {
            List<DA_Layer.Models.User> winnders = new List<DA_Layer.Models.User>();

            for (int i = 0; i < DAwinners.Count; ++i)
            {
                winnders.Add(DAwinners[i].GetDBUser());
            }

            return winnders;
        }

        private List<DA_Layer.Models.User> ConvertDrawsToDB(List<User> DAdraw)
        {
            List<DA_Layer.Models.User> draws = new List<DA_Layer.Models.User>();

            for (int i = 0; i < DAdraw.Count; ++i)
            {
                draws.Add(DAdraw[i].GetDBUser());
            }

            return draws;
        }
    }
}

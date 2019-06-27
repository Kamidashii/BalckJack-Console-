using BlackJackBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Models
{
    public class GameResult
    {
        public int GameId;
        public int AllGamesCount;

        public List<Interfaces.Models.IUser> Winners;
        public List<Interfaces.Models.IUser> Losers;
        public List<Interfaces.Models.IUser> Draws;

        public Croupier Croupier;


        public GameResult(int gameId)
        {
            this.GameId = gameId;

            this.Winners = new List<Interfaces.Models.IUser>();
            this.Losers = new List<Interfaces.Models.IUser>();
            this.Draws = new List<Interfaces.Models.IUser>();
        }

        public GameResult(int gameId,int allGamesCount,List<Interfaces.Models.IUser> winners,List<Interfaces.Models.IUser>losers,List<Interfaces.Models.IUser>draws,Croupier croupier)
        {
            this.GameId = gameId;
            this.AllGamesCount = allGamesCount;

            this.Winners = winners;
            this.Losers = losers;
            this.Draws = draws;

            this.Croupier = croupier;
        }
    }
}

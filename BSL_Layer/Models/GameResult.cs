using BlackJack_BSL.Interfaces;
using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Models
{
    public class GameResult
    {
        public int GameId;
        public int AllGamesCount;

        public List<IUser> Winners;
        public List<IUser> Losers;
        public List<IUser> Draws;

        public Croupier Croupier;


        public GameResult(int gameId)
        {
            this.GameId = gameId;

            this.Winners = new List<IUser>();
            this.Losers = new List<IUser>();
            this.Draws = new List<IUser>();
        }

        public GameResult(int gameId,int allGamesCount,List<IUser> winners,List<IUser>losers,List<IUser>draws,Croupier croupier)
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

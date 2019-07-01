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
            GameId = gameId;

            Winners = new List<Interfaces.Models.IUser>();
            Losers = new List<Interfaces.Models.IUser>();
            Draws = new List<Interfaces.Models.IUser>();
        }

        public GameResult(int gameId,int allGamesCount,List<Interfaces.Models.IUser> winners,List<Interfaces.Models.IUser>losers,List<Interfaces.Models.IUser>draws,Croupier croupier)
        {
            GameId = gameId;
            AllGamesCount = allGamesCount;

            Winners = winners;
            Losers = losers;
            Draws = draws;

            Croupier = croupier;
        }
    }
}

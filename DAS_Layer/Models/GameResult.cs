using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlackJack_DA.Models
{
    public class GameResult
    {
        public int GameId;
        public int AllGamesCount;

        public List<User> Winners;
        public List<User> Losers;
        public List<User> Draws;

        public Croupier Croupier;

        [JsonConstructor]
        public GameResult(int gameId)
        {
            this.GameId = gameId;

            this.Winners = new List<User>();
            this.Losers = new List<User>();
            this.Draws = new List<User>();
        }

        public GameResult(int gameId, int allGamesCount, List<User> winners, List<User> losers, List<User> draws, Croupier croupier)
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

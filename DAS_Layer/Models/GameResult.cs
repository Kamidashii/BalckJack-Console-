using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DA_Layer.Models
{
    public class GameResult
    {
        public int GameId;
        public int AllGamesCount;

        public List<User> Winners;
        public List<User> Losers;
        public List<User> Draw;

        public Croupier Croupier;

        [JsonConstructor]
        public GameResult(int gameId)
        {
            this.GameId = gameId;

            this.Winners = new List<User>();
            this.Losers = new List<User>();
            this.Draw = new List<User>();
        }
    }
}

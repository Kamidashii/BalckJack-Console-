using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.Models
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

using System;
using System.Collections.Generic;
using System.Linq;
using BlackJackDataAccess.Interfaces;
using BlackJackDataAccess.Interfaces.Repositories;
using BlackJackDataAccess.Models;

namespace BlackJackDataAccess.Repositories
{
    public class GameResultsRepository : IRepository<GameResult>
    {
        private List<GameResult> gameResults;

        public GameResultsRepository(List<GameResult> gameResults)
        {
            this.gameResults = gameResults;
        }

        public void Create(GameResult item)
        {
            this.gameResults.Add(item);
        }

        public GameResult Get(GameResult item)
        {
            return this.gameResults.Where(result => result.GameId == item.GameId).FirstOrDefault();
        }

        public IEnumerable<GameResult> GetAll()
        {
            return this.gameResults;
        }
    }
}

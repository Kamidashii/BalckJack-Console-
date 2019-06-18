using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Layer.Interfaces;
using DA_Layer.Models;

namespace DA_Layer.Repositories
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

        public IEnumerable<GameResult> GetAll()
        {
            return this.gameResults;
        }
    }
}

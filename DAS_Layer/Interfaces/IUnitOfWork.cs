using System;
using BlackJack_DA.Models;

namespace BlackJack_DA.Interfaces
{
    public interface IDataService
    {
        IRepository<GameResult> GameResultsRepository { get; }
        IRepository<Profile> ProfilesRepository { get; }
        void Save();
    }
}

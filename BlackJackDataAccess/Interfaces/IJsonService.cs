using System;
using BlackJackDataAccess.Models;

namespace BlackJackDataAccess.Interfaces
{
    public interface IJsonService
    {
        IRepository<GameResult> GameResultsRepository { get; }
        IRepository<Profile> ProfilesRepository { get; }
        void Save();
    }
}

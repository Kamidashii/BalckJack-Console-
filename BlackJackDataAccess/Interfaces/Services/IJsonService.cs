using System;
using BlackJackDataAccess.Interfaces.Repositories;
using BlackJackDataAccess.Models;

namespace BlackJackDataAccess.Interfaces.Services
{
    public interface IJsonService
    {
        IRepository<GameResult> GameResultsRepository { get; }
        IRepository<Profile> ProfilesRepository { get; }
        void Save();
    }
}

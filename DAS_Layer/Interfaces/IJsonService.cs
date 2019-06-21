using System;
using BlackJack_DA.Models;

namespace BlackJack_DA.Interfaces
{
    public interface IJsonService
    {
        IRepository<GameResult> GameResultsRepository { get; }
        IRepository<Profile> ProfilesRepository { get; }
        void Save();
    }
}

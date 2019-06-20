using System;
using DA_Layer.Models;

namespace DA_Layer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<GameResult> GameResultsRepository { get; }
        void Save();
    }
}

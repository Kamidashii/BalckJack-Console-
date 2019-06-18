using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_Layer.Models;

namespace DA_Layer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<GameResult> GameResultsRepository { get; }
        void Save();
    }
}

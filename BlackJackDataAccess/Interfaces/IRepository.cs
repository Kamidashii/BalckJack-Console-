using System;
using System.Collections.Generic;

namespace BlackJackDataAccess.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        T Get(T item);
    }
}

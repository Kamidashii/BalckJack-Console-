using System;
using System.Collections.Generic;

namespace BlackJack_DA.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Create(T item);
        T Get(T item);
    }
}

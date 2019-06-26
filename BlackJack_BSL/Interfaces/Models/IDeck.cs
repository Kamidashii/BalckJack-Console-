using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Interfaces.Models
{
    public interface IDeck
    {
        List<ICard> Cards { get; set; }

        ICard TakeCard();
    }
}

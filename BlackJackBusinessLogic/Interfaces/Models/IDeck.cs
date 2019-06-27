using System;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Interfaces.Models
{
    public interface IDeck
    {
        List<ICard> Cards { get; set; }

        ICard TakeCard();
    }
}

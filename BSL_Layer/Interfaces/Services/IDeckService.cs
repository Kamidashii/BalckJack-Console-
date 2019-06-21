using BlackJack_BSL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_BSL.Interfaces.Services
{
    public interface IDeckService
    {
        void SetAllCards(IDeck deck);
        void ShuffleCards(IDeck deck);
    }
}

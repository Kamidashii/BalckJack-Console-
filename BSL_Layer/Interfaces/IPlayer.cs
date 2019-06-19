using BSL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL_Layer.Interfaces
{
    public interface IPlayer
    {
        int Score { get; set; }
        bool IsBot { get; set; }
        List<ICard> Cards { get; set; }

        List<DA_Layer.Models.Card> ConvertCardsToDB();
        List<ICard> ConvertCardsFromDB(List<DA_Layer.Models.Card> DAcards);
    }
}

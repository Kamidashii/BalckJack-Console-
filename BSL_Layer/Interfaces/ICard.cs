using HelpfulValues.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL_Layer.Interfaces
{
    public interface ICard
    {
        Card_Enums.CardRank Rank { get; set; }
        Card_Enums.CardSuit Suit { get; set; }
        int Cost { get; }

        void DefineCost();

        DA_Layer.Models.Card GetDBCard();
    }
}

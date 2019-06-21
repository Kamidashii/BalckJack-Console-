using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Mappers
{
    public abstract class PlayerMapper
    {
        CardMapper cardMapper;

        public PlayerMapper()
        {
            this.cardMapper = new CardMapper();
        }
        protected List<BlackJack_DA.Models.Card> ConvertCardsToDA(List<BlackJack_BSL.Interfaces.ICard> BSLCards)
        {
            List<BlackJack_DA.Models.Card> DACards = new List<BlackJack_DA.Models.Card>();

            for (int i = 0; i < BSLCards.Count; ++i)
            {
                DACards.Add(cardMapper.ConvertItemToDA(BSLCards[i]));
            }

            return DACards;
        }

        protected List<BlackJack_BSL.Interfaces.ICard> ConvertCardsToBSL(List<BlackJack_DA.Models.Card> DACards)
        {
            List<BlackJack_BSL.Interfaces.ICard> BSLCards = new List<Interfaces.ICard>();

            for (int i = 0; i < DACards.Count; ++i)
            {
                BSLCards.Add(cardMapper.ConvertItemToBSL(DACards[i]));
            }

            return BSLCards;
        }
    }
}

using System;
using System.Collections.Generic;

namespace BlackJack_BSL.Mappers
{
    public abstract class PlayerMapper
    {
        private CardMapper _cardMapper;

        public PlayerMapper()
        {
            this._cardMapper = new CardMapper();
        }
        protected List<BlackJack_DA.Models.Card> ConvertCardsToDataAccess(List<BlackJack_BSL.Interfaces.Models.ICard> BusinessLogicCards)
        {
            List<BlackJack_DA.Models.Card> DataAccessCards = new List<BlackJack_DA.Models.Card>();

            for (int i = 0; i < BusinessLogicCards.Count; ++i)
            {
                DataAccessCards.Add(_cardMapper.ConvertItemToDataAccess(BusinessLogicCards[i]));
            }

            return DataAccessCards;
        }

        protected List<BlackJack_BSL.Interfaces.Models.ICard> ConvertCardsToBusinessLogic(List<BlackJack_DA.Models.Card> DataAccessCards)
        {
            List<BlackJack_BSL.Interfaces.Models.ICard> BusinessLogicCards = new List<Interfaces.Models.ICard>();

            for (int i = 0; i < DataAccessCards.Count; ++i)
            {
                BusinessLogicCards.Add(_cardMapper.ConvertItemToBusinessLogic(DataAccessCards[i]));
            }

            return BusinessLogicCards;
        }
    }
}

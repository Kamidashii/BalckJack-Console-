using System;
using System.Collections.Generic;

namespace BlackJackBusinessLogic.Mappers
{
    public abstract class PlayerMapper
    {
        private CardMapper _cardMapper;

        public PlayerMapper()
        {
            this._cardMapper = new CardMapper();
        }
        protected List<BlackJackDataAccess.Models.Card> ConvertCardsToDataAccess(List<BlackJackBusinessLogic.Interfaces.Models.ICard> BusinessLogicCards)
        {
            var DataAccessCards = new List<BlackJackDataAccess.Models.Card>();

            for (int i = 0; i < BusinessLogicCards.Count; ++i)
            {
                DataAccessCards.Add(_cardMapper.ConvertItemToDataAccess(BusinessLogicCards[i]));
            }

            return DataAccessCards;
        }

        protected List<BlackJackBusinessLogic.Interfaces.Models.ICard> ConvertCardsToBusinessLogic(List<BlackJackDataAccess.Models.Card> DataAccessCards)
        {
            var BusinessLogicCards = new List<Interfaces.Models.ICard>();

            for (int i = 0; i < DataAccessCards.Count; ++i)
            {
                BusinessLogicCards.Add(_cardMapper.ConvertItemToBusinessLogic(DataAccessCards[i]));
            }

            return BusinessLogicCards;
        }
    }
}

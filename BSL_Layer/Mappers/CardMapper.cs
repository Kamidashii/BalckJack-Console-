using System;

namespace BlackJack_BSL.Mappers
{
    public class CardMapper: Interfaces.IMapper<BlackJack_BSL.Interfaces.Models.ICard,BlackJack_DA.Models.Card>
    {

        public virtual BlackJack_BSL.Interfaces.Models.ICard ConvertItemToBusinessLogic(BlackJack_DA.Models.Card DataAccessCard)
        {
            BlackJack_BSL.Interfaces.Models.ICard BusinessLogicCard = new BlackJack_BSL.Models.Card(DataAccessCard.Rank, DataAccessCard.Suit);
            BusinessLogicCard.DefineCost();
            return BusinessLogicCard;
        }

        public virtual BlackJack_DA.Models.Card ConvertItemToDataAccess(BlackJack_BSL.Interfaces.Models.ICard BusinessLogicCard)
        {
            BlackJack_DA.Models.Card DataAccessCard = new BlackJack_DA.Models.Card(BusinessLogicCard.Rank, BusinessLogicCard.Suit);
            DataAccessCard.DefineCost();
            return DataAccessCard;
        }
        
    }
}

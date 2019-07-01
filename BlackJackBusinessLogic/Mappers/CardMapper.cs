using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class CardMapper: Interfaces.IMapper<BlackJackBusinessLogic.Interfaces.Models.ICard,BlackJackDataAccess.Models.Card>
    {

        public virtual BlackJackBusinessLogic.Interfaces.Models.ICard ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Card DataAccessCard)
        {
            var BusinessLogicCard = new BlackJackBusinessLogic.Models.Card(DataAccessCard.Rank, DataAccessCard.Suit);
            BusinessLogicCard.DefineCost();

            return BusinessLogicCard;
        }

        public virtual BlackJackDataAccess.Models.Card ConvertItemToDataAccess(BlackJackBusinessLogic.Interfaces.Models.ICard BusinessLogicCard)
        {
            var DataAccessCard = new BlackJackDataAccess.Models.Card(BusinessLogicCard.Rank, BusinessLogicCard.Suit);
            DataAccessCard.DefineCost();
            return DataAccessCard;
        }
        
    }
}

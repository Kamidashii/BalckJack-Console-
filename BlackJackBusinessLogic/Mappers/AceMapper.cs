using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class AceMapper : CardMapper, Interfaces.IMapper<BlackJackBusinessLogic.Interfaces.Models.ICard,BlackJackDataAccess.Models.Card>
    {
        public override BlackJackBusinessLogic.Interfaces.Models.ICard ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Card DataAccessCard)
        {
            var DataAccessAce = DataAccessCard as BlackJackDataAccess.Models.Ace;

            var BusinessLogicAce = base.ConvertItemToBusinessLogic(DataAccessCard) as BlackJackBusinessLogic.Models.Ace;
            BusinessLogicAce.IsSpecialOn = DataAccessAce.IsSpecialOn;
            BusinessLogicAce.SpecialCost = DataAccessAce.SpecialCost;

            return BusinessLogicAce;
        }

        public override BlackJackDataAccess.Models.Card ConvertItemToDataAccess(BlackJackBusinessLogic.Interfaces.Models.ICard BusinessLogicCard)
        {
            var BusinessLogicAce = BusinessLogicCard as BlackJackBusinessLogic.Models.Ace;

            var DataAccessAce = base.ConvertItemToDataAccess(BusinessLogicCard) as BlackJackDataAccess.Models.Ace;

            DataAccessAce.IsSpecialOn = BusinessLogicAce.IsSpecialOn;
            DataAccessAce.SpecialCost = BusinessLogicAce.SpecialCost;

            return DataAccessAce;
        }
    }
}

using System;

namespace BlackJack_BSL.Mappers
{
    public class AceMapper : CardMapper, Interfaces.IMapper<BlackJack_BSL.Interfaces.Models.ICard,BlackJack_DA.Models.Card>
    {
        public override BlackJack_BSL.Interfaces.Models.ICard ConvertItemToBusinessLogic(BlackJack_DA.Models.Card DataAccessCard)
        {
            BlackJack_DA.Models.Ace DataAccessAce = DataAccessCard as BlackJack_DA.Models.Ace;

            BlackJack_BSL.Models.Ace BusinessLogicAce = base.ConvertItemToBusinessLogic(DataAccessCard) as BlackJack_BSL.Models.Ace;
            BusinessLogicAce.IsSpecialOn = DataAccessAce.IsSpecialOn;
            BusinessLogicAce.SpecialCost = DataAccessAce.SpecialCost;

            return BusinessLogicAce;
        }

        public override BlackJack_DA.Models.Card ConvertItemToDataAccess(BlackJack_BSL.Interfaces.Models.ICard BusinessLogicCard)
        {
            BlackJack_BSL.Models.Ace BusinessLogicAce = BusinessLogicCard as BlackJack_BSL.Models.Ace;

            BlackJack_DA.Models.Ace DataAccessAce = base.ConvertItemToDataAccess(BusinessLogicCard) as BlackJack_DA.Models.Ace;

            DataAccessAce.IsSpecialOn = BusinessLogicAce.IsSpecialOn;
            DataAccessAce.SpecialCost = BusinessLogicAce.SpecialCost;

            return DataAccessAce;
        }
    }
}

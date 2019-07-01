using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class CroupierMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Models.Croupier,BlackJackDataAccess.Models.Croupier>
    {
        public BlackJackBusinessLogic.Models.Croupier ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Croupier DataAccessCroupier)
        {
            var cards = ConvertCardsToBusinessLogic(DataAccessCroupier.Cards);

            var BusinessLogicCroupier = new BlackJackBusinessLogic.Models.Croupier(DataAccessCroupier.Score, cards);

            return BusinessLogicCroupier;
        }

        public BlackJackDataAccess.Models.Croupier ConvertItemToDataAccess(BlackJackBusinessLogic.Models.Croupier BusinessLogicCroupier)
        {
            var cards = ConvertCardsToDataAccess(BusinessLogicCroupier.Cards);

            var DataAccessCroupier = new BlackJackDataAccess.Models.Croupier(BusinessLogicCroupier.Score, cards);

            return DataAccessCroupier;
        }
    }
}

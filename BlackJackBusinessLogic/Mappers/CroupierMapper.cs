using System;

namespace BlackJackBusinessLogic.Mappers
{
    public class CroupierMapper:PlayerMapper, Interfaces.IMapper<BlackJackBusinessLogic.Models.Croupier,BlackJackDataAccess.Models.Croupier>
    {
        public BlackJackBusinessLogic.Models.Croupier ConvertItemToBusinessLogic(BlackJackDataAccess.Models.Croupier DataAccessCroupier)
        {
            BlackJackBusinessLogic.Models.Croupier BusinessLogicCroupier = new BlackJackBusinessLogic.Models.Croupier(DataAccessCroupier.Score, ConvertCardsToBusinessLogic(DataAccessCroupier.Cards));

            return BusinessLogicCroupier;
        }

        public BlackJackDataAccess.Models.Croupier ConvertItemToDataAccess(BlackJackBusinessLogic.Models.Croupier BusinessLogicCroupier)
        {
            BlackJackDataAccess.Models.Croupier DataAccessCroupier = new BlackJackDataAccess.Models.Croupier(BusinessLogicCroupier.Score, ConvertCardsToDataAccess(BusinessLogicCroupier.Cards));

            return DataAccessCroupier;
        }
    }
}

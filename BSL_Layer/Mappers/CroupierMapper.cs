using System;

namespace BlackJack_BSL.Mappers
{
    public class CroupierMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Models.Croupier,BlackJack_DA.Models.Croupier>
    {
        public BlackJack_BSL.Models.Croupier ConvertItemToBusinessLogic(BlackJack_DA.Models.Croupier DataAccessCroupier)
        {
            BlackJack_BSL.Models.Croupier BusinessLogicCroupier = new BlackJack_BSL.Models.Croupier(DataAccessCroupier.Score, ConvertCardsToBusinessLogic(DataAccessCroupier.Cards));

            return BusinessLogicCroupier;
        }

        public BlackJack_DA.Models.Croupier ConvertItemToDataAccess(BlackJack_BSL.Models.Croupier BusinessLogicCroupier)
        {
            BlackJack_DA.Models.Croupier DataAccessCroupier = new BlackJack_DA.Models.Croupier(BusinessLogicCroupier.Score, ConvertCardsToDataAccess(BusinessLogicCroupier.Cards));

            return DataAccessCroupier;
        }
    }
}

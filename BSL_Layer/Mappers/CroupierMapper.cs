using System;

namespace BlackJack_BSL.Mappers
{
    public class CroupierMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Models.Croupier,BlackJack_DA.Models.Croupier>
    {
        public BlackJack_BSL.Models.Croupier ConvertItemToBSL(BlackJack_DA.Models.Croupier DACroupier)
        {
            BlackJack_BSL.Models.Croupier BSLCroupier = new BlackJack_BSL.Models.Croupier(DACroupier.Score, ConvertCardsToBSL(DACroupier.Cards));

            return BSLCroupier;
        }

        public BlackJack_DA.Models.Croupier ConvertItemToDA(BlackJack_BSL.Models.Croupier BSLCroupier)
        {
            BlackJack_DA.Models.Croupier DACroupier = new BlackJack_DA.Models.Croupier(BSLCroupier.Score, ConvertCardsToDA(BSLCroupier.Cards));

            return DACroupier;
        }
    }
}

using System;

namespace BlackJack_BSL.Mappers
{
    public class AceMapper : CardMapper, Interfaces.IMapper<BlackJack_BSL.Interfaces.ICard,BlackJack_DA.Models.Card>
    {
        public override BlackJack_BSL.Interfaces.ICard ConvertItemToBSL(BlackJack_DA.Models.Card DACard)
        {
            BlackJack_DA.Models.Ace DAAce = DACard as BlackJack_DA.Models.Ace;

            BlackJack_BSL.Models.Ace BSLAce = base.ConvertItemToBSL(DACard) as BlackJack_BSL.Models.Ace;
            BSLAce.IsSpecialOn = DAAce.IsSpecialOn;
            BSLAce.SpecialCost = DAAce.SpecialCost;

            return BSLAce;
        }

        public override BlackJack_DA.Models.Card ConvertItemToDA(BlackJack_BSL.Interfaces.ICard BSLCard)
        {
            BlackJack_BSL.Models.Ace BSLAce = BSLCard as BlackJack_BSL.Models.Ace;

            BlackJack_DA.Models.Ace DAAce = base.ConvertItemToDA(BSLCard) as BlackJack_DA.Models.Ace;

            DAAce.IsSpecialOn = BSLAce.IsSpecialOn;
            DAAce.SpecialCost = BSLAce.SpecialCost;

            return DAAce;
        }
    }
}

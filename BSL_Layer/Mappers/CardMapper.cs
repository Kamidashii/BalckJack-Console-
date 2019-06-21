using System;

namespace BlackJack_BSL.Mappers
{
    public class CardMapper: Interfaces.IMapper<BlackJack_BSL.Interfaces.ICard,BlackJack_DA.Models.Card>
    {

        public virtual BlackJack_BSL.Interfaces.ICard ConvertItemToBSL(BlackJack_DA.Models.Card DACard)
        {
            BlackJack_BSL.Interfaces.ICard BSLCard = new BlackJack_BSL.Models.Card(DACard.Rank, DACard.Suit);
            BSLCard.DefineCost();
            return BSLCard;
        }

        public virtual BlackJack_DA.Models.Card ConvertItemToDA(BlackJack_BSL.Interfaces.ICard BSLCard)
        {
            BlackJack_DA.Models.Card DAcard = new BlackJack_DA.Models.Card(BSLCard.Rank, BSLCard.Suit);
            DAcard.DefineCost();
            return DAcard;
        }
        
    }
}

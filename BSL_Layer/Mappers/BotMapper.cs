using System;

namespace BlackJack_BSL.Mappers
{
    public class BotMapper:PlayerMapper, Interfaces.IMapper<BlackJack_BSL.Models.Bot,BlackJack_DA.Models.Bot>
    {
        public BlackJack_BSL.Models.Bot ConvertItemToBSL(BlackJack_DA.Models.Bot DABot)
        {
            BlackJack_BSL.Models.Bot BSLBot = new BlackJack_BSL.Models.Bot(DABot.Name, DABot.Bet,DABot.Demeanor, DABot.Score, ConvertCardsToBSL(DABot.Cards),DABot.IsBot);

            return BSLBot;
        }

        public BlackJack_DA.Models.Bot ConvertItemToDA(BlackJack_BSL.Models.Bot BSLBot)
        {
            BlackJack_DA.Models.Bot DABot = new BlackJack_DA.Models.Bot(BSLBot.Name, BSLBot.Bet, BSLBot.Demeanor, BSLBot.Score, ConvertCardsToDA(BSLBot.Cards), BSLBot.IsBot);

            return DABot;
        }
    }
}

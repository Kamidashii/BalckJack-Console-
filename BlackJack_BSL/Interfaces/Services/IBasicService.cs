using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack_BSL.Interfaces.Services
{
    public interface IBasicService
    {
        IMapper<BlackJack_BSL.Interfaces.Models.ICard, BlackJack_DA.Models.Card> AceMapper { get; set; }

        IMapper<BlackJack_BSL.Models.Bot, BlackJack_DA.Models.Bot> BotMapper { get; set; }

        IMapper<Interfaces.Models.ICard, BlackJack_DA.Models.Card> CardMapper { get; set; }

        IMapper<BlackJack_BSL.Models.Croupier, BlackJack_DA.Models.Croupier> CroupierMapper { get; set; }

        IMapper<BlackJack_BSL.Models.GameResult, BlackJack_DA.Models.GameResult> GameResultMapper { get; set; }

        IMapper<BlackJack_BSL.Interfaces.Models.IProfile, BlackJack_DA.Models.Profile> ProfileMapper { get; set; }

        IMapper<BlackJack_BSL.Interfaces.Models.IUser, BlackJack_DA.Models.User> UserMapper { get; set; }
        
        



        Interfaces.Models.IUser GetPlayerByProfile(Interfaces.Models.IProfile playerProfile);

        void PlayerGetCard(Interfaces.Models.IPlayer player, Interfaces.Models.ICard card);

        Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original);

        void ResetPlayerDeck(Interfaces.Models.IPlayer player);

        void ResetPlayerScore(Interfaces.Models.IPlayer player);

        Interfaces.Models.ICard PullOutCard();

        List<Interfaces.Models.IAce> CountAces(Interfaces.Models.IPlayer player);

        bool IsPlayerWonScore(Interfaces.Models.IPlayer player);

        bool IsPlayerScoreValid(Interfaces.Models.IPlayer player);

        void RecalculateScore(Interfaces.Models.IPlayer player);
    }
}

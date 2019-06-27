using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackBusinessLogic.Interfaces.Services
{
    public interface IBasicService
    {

        IMapper<BlackJackBusinessLogic.Models.GameResult, BlackJackDataAccess.Models.GameResult> GameResultMapper { get; set; }



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

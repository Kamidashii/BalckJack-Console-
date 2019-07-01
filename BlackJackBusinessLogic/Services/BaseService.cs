using System;
using BlackJackBusinessLogic.Models;
using System.Collections.Generic;
using Common.Constants;
using Common.Enums;
using BlackJackBusinessLogic.Interfaces;
using BlackJackDataAccess;
using BlackJackBusinessLogic.Mappers;
using System.Linq;
using BlackJackBusinessLogic.Interfaces.Models;
using BlackJackDataAccess.Services;

namespace BlackJackBusinessLogic.Services
{
    public class BaseService : Interfaces.Services.IBasicService
    {

        protected IMapper<BlackJackBusinessLogic.Interfaces.Models.ICard, BlackJackDataAccess.Models.Card> AceMapper;
        protected IMapper<BlackJackBusinessLogic.Models.Bot, BlackJackDataAccess.Models.Bot> BotMapper;
        protected IMapper<Interfaces.Models.ICard, BlackJackDataAccess.Models.Card> CardMapper;
        protected IMapper<BlackJackBusinessLogic.Models.Croupier, BlackJackDataAccess.Models.Croupier> CroupierMapper;
        protected IMapper<BlackJackBusinessLogic.Interfaces.Models.IProfile, BlackJackDataAccess.Models.Profile> ProfileMapper;
        protected IMapper<BlackJackBusinessLogic.Interfaces.Models.IUser, BlackJackDataAccess.Models.User> UserMapper;

        public IMapper<BlackJackBusinessLogic.Models.GameResult, BlackJackDataAccess.Models.GameResult> GameResultMapper { get; set; }

        protected JsonService jsonService;

        public BaseService()
        {

            jsonService = new JsonService();

            AceMapper = new AceMapper();
            BotMapper = new BotMapper();
            CardMapper = new CardMapper();
            CroupierMapper = new CroupierMapper();
            GameResultMapper = new GameResultMapper();
            ProfileMapper = new ProfileMapper();
            UserMapper = new UserMapper();
        }

        public void RecalculateScore(Interfaces.Models.IPlayer player)
        {
            if (IsPlayerScoreValid(player))
            {
                return;
            }

            var aces = CountAces(player);
            for (int i = 0; i < aces.Count && !IsPlayerScoreValid(player); i++)
            {
                player.Score -= aces[i].GetSpecialCostDifference();
            }
        }

        public bool IsPlayerWonScore(Interfaces.Models.IPlayer player)
        {
            return player.Score == GameService_Constants.MaxValidScore;
        }

        public bool IsPlayerScoreValid(Interfaces.Models.IPlayer player)
        {
            return player.Score <= GameService_Constants.MaxValidScore;
        }

        public List<Interfaces.Models.IAce> CountAces(Interfaces.Models.IPlayer player)
        {
            var aces = player.Cards.Where(
                card => card.Rank == CardRanks.CardRank.Ace
                ).Cast<Interfaces.Models.IAce>().ToList();

            DeleteActivatedAces(aces);
            ActivateAces(aces);

            return aces;
        }


        private void DeleteActivatedAces(List<Interfaces.Models.IAce> aces)
        {
            for (int i = 0; i < aces.Count; ++i)
            {
                if (aces[i].IsSpecialOn)
                {
                    aces.RemoveAt(i);
                }
            }
        }

        private void ActivateAces(List<Interfaces.Models.IAce> aces)
        {
            for (int i = 0; i < aces.Count; ++i)
            {
                aces[i].IsSpecialOn = true;
            }
        }

        public void ResetPlayerScore(Interfaces.Models.IPlayer player)
        {
            player.Score = 0;
        }

        public void ResetPlayerDeck(Interfaces.Models.IPlayer player)
        {
            player.Cards = new List<Interfaces.Models.ICard>();
        }

        public virtual Interfaces.Models.IPlayer MakePlayerClone(Interfaces.Models.IPlayer original) { return null; }

        public void PlayerGetCard(Interfaces.Models.IPlayer player, Interfaces.Models.ICard card)
        {
            player.Cards.Add(card);
            player.Score += card.Cost;
        }

        public Interfaces.Models.IUser GetPlayerByProfile(Interfaces.Models.IProfile playerProfile)
        {
            var profile = jsonService.ProfilesRepository.Get(ProfileMapper.ConvertItemToDataAccess(playerProfile));

            if (profile == null)
            {
                return null;
            }


            var DataAccessUser = profile.User;

            return UserMapper.ConvertItemToBusinessLogic(DataAccessUser);
        }
    }
}

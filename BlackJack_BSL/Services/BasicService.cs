using System;
using BlackJack_BSL.Models;
using System.Collections.Generic;
using Common.Constants;
using Common.Enums;
using BlackJack_BSL.Interfaces;
using BlackJack_DA;
using BlackJack_BSL.Mappers;
using System.Linq;
using BlackJack_BSL.Interfaces.Models;

namespace BlackJack_BSL.Services
{
    public class BasicService : Interfaces.Services.IBasicService
    {
        protected List<Interfaces.Models.IUser> players;
        protected List<IDeck> decks;
        protected Interfaces.Models.IPlayer croupier;

        protected IMapper<BlackJack_BSL.Interfaces.Models.ICard, BlackJack_DA.Models.Card> AceMapper;
        protected IMapper<BlackJack_BSL.Models.Bot, BlackJack_DA.Models.Bot> BotMapper;
        protected IMapper<Interfaces.Models.ICard, BlackJack_DA.Models.Card> CardMapper;
        protected IMapper<BlackJack_BSL.Models.Croupier, BlackJack_DA.Models.Croupier> CroupierMapper;
        protected IMapper<BlackJack_BSL.Interfaces.Models.IProfile, BlackJack_DA.Models.Profile> ProfileMapper;
        protected IMapper<BlackJack_BSL.Interfaces.Models.IUser, BlackJack_DA.Models.User> UserMapper;

        public IMapper<BlackJack_BSL.Models.GameResult, BlackJack_DA.Models.GameResult> GameResultMapper { get; set; }

        protected JsonService jsonService;

        public BasicService(List<BlackJack_BSL.Interfaces.Models.IUser> players, List<IDeck> decks, Interfaces.Models.IPlayer croupier)
        {
            this.players = players;
            this.decks = decks;
            this.croupier = croupier;

            jsonService = new JsonService();

            this.AceMapper = new AceMapper();
            this.BotMapper = new BotMapper();
            this.CardMapper = new CardMapper();
            this.CroupierMapper = new CroupierMapper();
            this.GameResultMapper = new GameResultMapper();
            this.ProfileMapper = new ProfileMapper();
            this.UserMapper = new UserMapper();
        }

        public void RecalculateScore(Interfaces.Models.IPlayer player)
        {
            if (IsPlayerScoreValid(player)) return;

            List<Interfaces.Models.IAce> aces = CountAces(player);
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
            List<Interfaces.Models.IAce> aces = player.Cards.Where(
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

        public Interfaces.Models.ICard PullOutCard()
        {
            Random random = new Random();
            IDeck randomDeck = decks[random.Next(0, decks.Count)];
            Interfaces.Models.ICard randomCard = randomDeck.TakeCard();

            return randomCard;
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
            BlackJack_DA.Models.Profile profile = jsonService.ProfilesRepository.Get(ProfileMapper.ConvertItemToDataAccess(playerProfile));

            if (profile == null) return null;


            BlackJack_DA.Models.User DAuser = profile.User;

            return UserMapper.ConvertItemToBusinessLogic(DAuser);
        }
    }
}

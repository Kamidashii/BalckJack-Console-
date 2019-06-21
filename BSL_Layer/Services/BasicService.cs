using System;
using BlackJack_BSL.Models;
using System.Collections.Generic;
using Common.Constants;
using Common.Enums;
using BlackJack_BSL.Interfaces;
using BlackJack_DA;
using BlackJack_BSL.Mappers;

namespace BlackJack_BSL.Services
{
    public class BasicService : IPlayerService
    {
        protected List<IUser> players;
        protected List<Deck> decks;
        protected IPlayer croupier;

        #region Mappers
        public IMapper<BlackJack_BSL.Interfaces.ICard, BlackJack_DA.Models.Card> AceMapper;
        public IMapper<BlackJack_BSL.Models.Bot, BlackJack_DA.Models.Bot> BotMapper;
        public IMapper<Interfaces.ICard, BlackJack_DA.Models.Card> CardMapper;
        public IMapper<BlackJack_BSL.Models.Croupier, BlackJack_DA.Models.Croupier> CroupierMapper;
        public IMapper<BlackJack_BSL.Models.GameResult, BlackJack_DA.Models.GameResult> GameResultMapper;
        public IMapper<BlackJack_BSL.Interfaces.IProfile, BlackJack_DA.Models.Profile> ProfileMapper;
        public IMapper<BlackJack_BSL.Interfaces.IUser, BlackJack_DA.Models.User> UserMapper;
        #endregion

        JsonService jsonService;

        public BasicService(List<IUser> players, List<Deck> decks, IPlayer croupier)
        {
            this.players = players;
            this.decks = decks;
            this.croupier = croupier;

            jsonService = new JsonService();

            #region MappersInitialize
            this.AceMapper = new AceMapper();
            this.BotMapper = new BotMapper();
            this.CardMapper = new CardMapper();
            this.CroupierMapper = new CroupierMapper();
            this.GameResultMapper = new GameResultMapper();
            this.ProfileMapper = new ProfileMapper();
            this.UserMapper = new UserMapper();
            #endregion 
        }

        public void RecalculateScore(IPlayer player)
        {
            if (IsPlayerScoreValid(player)) return;

            List<Ace> aces = CountAces(player);
            for (int i = 0; i < aces.Count && !IsPlayerScoreValid(player); i++)
            {
                player.Score -= aces[i].GetSpecialCostDifference();
            }
        }

        public bool IsPlayerWonScore(IPlayer player)
        {
            return player.Score == GameService_Constants.MaxValidScore;
        }

        public bool IsPlayerScoreValid(IPlayer player) //If Score more then 21 returns false
        {
            return player.Score <= GameService_Constants.MaxValidScore;
        }

        protected List<Ace> CountAces(IPlayer player)
        {
            List<Ace> aces = new List<Ace>();
            for (int i = 0; i < player.Cards.Count; ++i)
            {
                if (player.Cards[i].Rank == CardRanks.CardRank.Ace)
                {
                    Ace ace = player.Cards[i] as Ace;
                    if (!ace.IsSpecialOn)
                    {
                        aces.Add(player.Cards[i] as Ace);
                        ace.IsSpecialOn = true;
                    }
                }
            }
            return aces;
        }

        public ICard PullOutCard()
        {
            Random random = new Random();
            Deck randomDeck = decks[random.Next(0, decks.Count)];
            ICard randomCard = randomDeck.TakeCard();

            return randomCard;
        }

        public void ResetPlayerScore(IPlayer player)
        {
            player.Score = 0;
        }

        public void ResetPlayerDeck(IPlayer player)
        {
            player.Cards = new List<ICard>();
        }

        public virtual IPlayer MakePlayerClone(IPlayer original) { return null; }

        public void PlayerGetCard(IPlayer player, ICard card)
        {
            player.Cards.Add(card);
            player.Score += card.Cost;
        }

        public IUser GetPlayerByProfile(IProfile playerProfile)
        {
            BlackJack_DA.Models.Profile profile = jsonService.ProfilesRepository.Get(ProfileMapper.ConvertItemToDA(playerProfile));

            if (profile == null) return null;


            BlackJack_DA.Models.User DAuser = profile.User;

            return UserMapper.ConvertItemToBSL(DAuser);
        }
    }
}

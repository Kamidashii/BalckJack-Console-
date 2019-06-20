using System;
using BSL_Layer.Models;
using System.Collections.Generic;
using HelpfulValues.Constants;
using HelpfulValues.Enums;
using BSL_Layer.Interfaces;
using DA_Layer;

namespace BSL_Layer.Services
{
    public class BasicService : IPlayerService
    {
        protected List<IPlayer> players;
        protected List<Deck> decks;
        protected IPlayer croupier;

        JSonUnitOfWork jSonUnitOfWork;

        public BasicService(List<IPlayer> players, List<Deck> decks, IPlayer croupier)
        {
            this.players = players;
            this.decks = decks;
            this.croupier = croupier;

            jSonUnitOfWork = new JSonUnitOfWork();
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
            return player.Score == GameService_Constants.MAX_VALID_SCORE;
        }

        public bool IsPlayerScoreValid(IPlayer player) //If Score more then 21 returns false
        {
            return player.Score <= GameService_Constants.MAX_VALID_SCORE;
        }

        protected List<Ace> CountAces(IPlayer player)
        {
            List<Ace> aces = new List<Ace>();
            for (int i = 0; i < player.Cards.Count; ++i)
            {
                if (player.Cards[i].Rank == Card_Enums.CardRank.Ace)
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

        public User GetPlayerByProfile(IProfile playerProfile)
        {
            DA_Layer.Models.Profile profile = jSonUnitOfWork.ProfilesRepository.Get(playerProfile.GetProfileToDB());

            if (profile == null) return null;


            DA_Layer.Models.User DAuser = profile.User;

            return new User(DAuser);
        }
    }
}

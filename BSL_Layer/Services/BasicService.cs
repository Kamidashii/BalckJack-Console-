using BSL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpfulValues.Constants;
using HelpfulValues.Enums;

namespace BSL_Layer.Services
{
    public class BasicService
    {
        protected List<User> players;
        protected List<Deck> decks;
        protected Croupier croupier;

        public BasicService(List<User> players, List<Deck> decks, Croupier croupier)
        {
            this.players = players;
            this.decks = decks;
            this.croupier = croupier;
        }

        public void RecalculateScore(Player player)
        {
            if (IsPlayerScoreValid(player)) return;

            List<Ace> aces = CountAces(player);
            for (int i = 0; i < aces.Count && !IsPlayerScoreValid(player); i++)
            {
                player.Score -= aces[i].GetSpecialCostDifference();
            }
        }

        public bool IsPlayerWonScore(Player player)
        {
            return player.Score == GameService_Constants.MAX_VALID_SCORE;
        }

        public bool IsPlayerScoreValid(Player player) //If Score more then 21 returns false
        {
            return player.Score <= GameService_Constants.MAX_VALID_SCORE;
        }

        protected List<Ace> CountAces(Player player)
        {
            List<Ace> aces = new List<Ace>();
            for (int i = 0; i < player.Cards.Count; ++i)
            {
                if (player.Cards[i].Rank == Card_Enums.CardRank.Ace)
                {
                    Ace ace = player.Cards[i] as Ace;
                    if (!ace.IsSpecialOn)
                        aces.Add(player.Cards[i] as Ace);
                    else
                    {
                        ace.IsSpecialOn = true;
                    }
                }
            }
            return aces;
        }

        public Card PullOutCard()
        {
            Random random = new Random();
            Deck randomDeck = decks[random.Next(0, decks.Count)];
            Card randomCard = randomDeck.TakeCard();

            return randomCard;
        }

        public void ResetPlayerScore(Player player)
        {
            player.Score = 0;
        }

        public void ResetPlayerDeck(Player player)
        {
            player.Cards = new List<Card>();
        }

        public virtual Player MakePlayerClone(Player original) { return null; }
    }
}

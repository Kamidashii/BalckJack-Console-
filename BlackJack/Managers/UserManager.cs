using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack.Managers
{
    public class UserManager : BasicManager
    {
        public UserManager(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier) { }

        public void StartUserTurn(User user)
        {
            Enums.User_Enums.ActionType choosedAction = Enums.User_Enums.ActionType.Invalid;
            MainView.ShowUserTurn(user);
            do
            {
                MainView.ShowUserActionInstruction();
                choosedAction = MainView.FindOutUserAction();

                switch (choosedAction)
                {
                    case Enums.User_Enums.ActionType.Take:
                        {
                            UserGetCard(user, PullOutCard());

                            MainView.ShowUserSpecificCardGetting(user);

                            RecalculateScore(user);
                            MainView.ShowUserScore(user);

                            if (!IsPlayerScoreValid(user))
                            {
                                MainView.ShowOverfeedScoreMessage(user);
                                choosedAction = Enums.User_Enums.ActionType.Finished;
                            }

                            else if (IsPlayerWonScore(user))
                            {
                                MainView.ShowGreatScoreMessage(user);
                                choosedAction = Enums.User_Enums.ActionType.Finished;
                            }
                        }
                        break;
                    case Enums.User_Enums.ActionType.Enough:
                        {
                            choosedAction = Enums.User_Enums.ActionType.Finished;
                        }
                        break;
                    case Enums.User_Enums.ActionType.Surrender:
                        {
                            MainView.ShowUserLost(user);
                            choosedAction = Enums.User_Enums.ActionType.Finished;
                        }
                        break;
                    case Enums.User_Enums.ActionType.ShowCards:
                        {
                            ShowUserOwnedCards(user);
                        }
                        break;
                    default:
                        {
                            MainView.ShowInvalidChoose();
                            choosedAction = Enums.User_Enums.ActionType.Invalid;
                        }
                        break;
                }

            } while (choosedAction != Enums.User_Enums.ActionType.Finished);
        }

        public void UserGetCard(User user, Card card)
        {
            user.Cards.Add(card);
            user.Score += card.GetCost();

            MainView.ShowUserCardGetting(user);
        }

        public void ShowUserOwnedCards(User user)
        {
            for (int i = 0; i < user.Cards.Count; ++i)
            {
                MainView.ShowCard(user.Cards[i]);
            }
        }

        public void PlayerGetCard(User user, Card card)
        {
            user.Cards.Add(card);
            user.Score += card.GetCost();
            MainView.ShowUserCardGetting(user);
        }

        public override Player MakePlayerClone(Player original)
        {
            User origin = original as User;
            Player copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards);
            return copy;
        }
    }
}

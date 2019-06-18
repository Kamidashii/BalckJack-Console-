using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSL_Layer.Models;
using HelpfulValues.Constants;
using HelpfulValues.Enums;


namespace BSL_Layer.Services
{
    public class UserService : BasicService
    {
        public UserService(List<User> players, List<Deck> decks, Croupier croupier) : base(players, decks, croupier) { }

        

        public void UserGetCard(User user, Card card)
        {
            user.Cards.Add(card);
            user.Score += card.GetCost();
        }

        public override Player MakePlayerClone(Player original)
        {
            User origin = original as User;
            Player copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards);
            return copy;
        }
    }
}

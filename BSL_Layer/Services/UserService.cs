using System;
using System.Collections.Generic;
using BlackJack_BSL.Interfaces;
using BlackJack_BSL.Models;


namespace BlackJack_BSL.Services
{
    public class UserService : BasicService
    {
        public UserService(List<IUser> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier) { }
        

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            IUser origin = original as User;
            IUser copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards,false);
            return copy;
        }
    }
}

using System;
using System.Collections.Generic;
using BSL_Layer.Interfaces;
using BSL_Layer.Models;


namespace BSL_Layer.Services
{
    public class UserService : BasicService
    {
        public UserService(List<IPlayer> players, List<Deck> decks, IPlayer croupier) : base(players, decks, croupier) { }
        

        public override IPlayer MakePlayerClone(IPlayer original)
        {
            User origin = original as User;
            IPlayer copy = new User(origin.Name, origin.Bet, origin.Score, origin.Cards);
            return copy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>() { new User("Vasya", 200) };
            Croupier croupier = new Croupier();
            List<Deck> decks = new List<Deck> { new Deck() };

            GameController.GetInstance().SetData(users, croupier, decks);

            GameController.GetInstance().StartGame();

            Console.Read();
        }
    }
}

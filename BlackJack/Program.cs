using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Views;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>() { new User("Vasya", 200),
            new Bot("CalmBot",200,Enums.Bot_Enums.Bot_Demeanor.Safe),
            new Bot("NormalBot",200,Enums.Bot_Enums.Bot_Demeanor.Normal),
            new Bot("DesperateBot",200,Enums.Bot_Enums.Bot_Demeanor.Desperate)};


            Croupier croupier = new Croupier();
            MainView mainView = new MainView();

            GameController.GetInstance().SetData(users, croupier,4,1, mainView);

            GameController.GetInstance().StartGames();

            Console.Read();
        }
    }
}

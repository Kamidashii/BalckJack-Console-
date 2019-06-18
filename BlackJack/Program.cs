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
            new Bot("CalmBot",50,Enums.Bot_Enums.Bot_Demeanor.Safe),
            new Bot("NormalBot",100,Enums.Bot_Enums.Bot_Demeanor.Normal),
            new Bot("DesperateBot",400,Enums.Bot_Enums.Bot_Demeanor.Desperate),
            new Bot("NormalBot2", 100,Enums.Bot_Enums.Bot_Demeanor.Normal)};


            Croupier croupier = new Croupier();

            GameController.GetInstance().SetData(users, croupier, 2, 1);

            GameController.GetInstance().StartGames();

            Console.Read();
        }
    }
}

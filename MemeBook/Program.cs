using System;
using System.Collections.Generic;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;

namespace MemeBook
{
    class Program
    {
        private static User _user;
        private static string refId;
        private static UserService _uService;
        private static PostService _pService;
        private static CircleService _cService;
        private static View _view;
        static void Main(string[] args)
        {
            _uService = new UserService();
            _pService = new PostService();
            _cService = new CircleService();
            _view = new View();
            _uService.KillAllUsers();
            _pService.KillAllPosts();
            _cService.KillAllCircles();

            DataSeeding mySeed = new DataSeeding();
            mySeed.seedUsers();
            mySeed.SeedCircle();
            mySeed.SeedPosts();

            Console.WriteLine("Welcome to memebook");
            Console.WriteLine("Enter username");
            refId = _uService.GetByName(Console.ReadLine());
            Console.WriteLine("Enter password");
            if (Console.ReadLine() == "Hej")
            {
                _user = _uService.GetById(refId);
                Console.WriteLine("Now logged in");
            }

            bool fuckYouuuuu = false;

            do
            {
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key)) continue;
                switch (key[0])
                {
                    case 'w':
                    {
                        var myPost = _view.Wall(_user);
                        Console.WriteLine(myPost.Count);
                        Console.WriteLine(_user.User_ID);
                        Console.WriteLine("I Entered an evul loop");
                        foreach (var i in myPost)
                        {
                            Console.Write("Derpy mc Derp \n");
                            Console.WriteLine(i.Content.ToString());
                        }
                        Console.Write("Breaking a habit");
                        break;
                    }
                    case 'f':
                    {
                        break;
                    }
                    case 'u':
                    {
                        break;
                    }
                }
            } while (!fuckYouuuuu);
        }
    }
}

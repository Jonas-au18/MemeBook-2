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

            bool done = false;

            do
            {
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key)) continue;
                switch (key[0])
                {
                    case 'w':
                    {
                        var myPost = _view.Wall(_user);

                        foreach (var i in myPost)
                        {
                            Console.WriteLine(i.Content);
                        }

                        break;
                    }
                    case 'f':
                    {
                        var myFeed = _view.Feed(_user);
                        foreach (var i in myFeed)
                        {
                            Console.WriteLine(i.Content);   
                        }
                        break;
                    }
                    case 'u':
                    {
                        break;
                    }
                }
            } while (!done);
        }
    }
}

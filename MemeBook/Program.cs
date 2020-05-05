using System;
using System.Collections.Generic;
using System.Diagnostics;
using MemeBook.Controller;
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
        private static LoginService _lService;
        private static View _view;
        private static MemeBookController _control;
        static void Main(string[] args)
        {
            DataSeeding.seed();
            _control = new MemeBookController();


            Console.WriteLine("Welcome to memebook, the place where consoles meet");

            bool done = false;

            do
            {
                _control.DefaultInterface();
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key)) continue;
                switch (key[0])
                {
                    case 'w':
                    {
                        var myPost = _view.Wall(_user.PersonalCircle);
                        Console.WriteLine("\n");
                        foreach (var i in myPost)
                        {
                            Console.WriteLine(i.Content + " " + i.date + " " + _uService.GetById(i.Owner_ID).Fullname);
                        }
                        Console.WriteLine("\n");
                        break;
                    }
                    case 'f':
                    {
                        var myFeed = _view.Feed(_user);
                        Console.WriteLine("\n");
                        foreach (var i in myFeed)
                        {
                            Console.WriteLine(i.Content);   
                        }
                        Console.WriteLine("\n");
                        break;
                    }
                    case 'u':
                    {
                        _control.SearchUser();
                        break;
                    }
                    case 'l':
                    {
                        _control.Login();
                        break;
                    }
                }
            } while (!done);
        }
    }
}

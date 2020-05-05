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
                    case 'e': // search group
                    {
                        _control.ShowFriends();
                        break;
                    }
                    case 'g': // search group
                    {
                        _control.SearchGroup();
                        break;
                    }
                    case 'w':
                    {
                        _control.wall();
                        break;
                    }
                    case 'f':
                    {
                        _control.feed();
                        
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
                    case 'o':
                    {
                        _control.logout();
                        done = true;
                        break;
                    }
                    case 'h':
                        _control.ShowGroups();
                        break;
                }
            } while (!done);
        }
    }
}

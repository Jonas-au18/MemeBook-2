using System;
using System.Collections.Generic;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;

namespace MemeBook.Controller
{
    public class MemeBookController
    {
        private User _user;
        private string refId;
        private UserService _uService;
        private PostService _pService;
        private CircleService _cService;
        private LoginService _lService;
        private View _view;

        public MemeBookController()
        {
            _uService = new UserService();
            _pService = new PostService();
            _cService = new CircleService();
            _lService = new LoginService();
            _view = new View();
        }

        public void boxLine()
        {
            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public void DefaultInterface()
        {
            Console.WriteLine("|| u:Search user || g:Search group || w:View wall || f:View feed || l:Login ||"); //@ max len, new line after hits.
            Console.WriteLine("|| e:Show friends|| h: Show groups || o:Logout    ||");
            boxLine();
        }

        public void Login()
        {
            boxLine();
            Console.WriteLine("Enter username");
            boxLine();
            refId = _lService.GetByName(Console.ReadLine());
            boxLine();
            Console.WriteLine("Enter password");
            boxLine();
            var id = _lService.validate(Console.ReadLine(), refId);
            if (id != null)
            {
                _user = _uService.GetById(refId); 
                boxLine();
                Console.WriteLine("Now logged in as: " + _uService.GetById(_user.User_ID).Fullname);
                boxLine();
                _user.Logged_in = true;
                _uService.UpdateUser(_user);
            }
        }

        public void ShowFriends()
        {
            if (_user.Following != null)
            {
                foreach (var i in _user.Following)
                {
                    var friend = _uService.GetById(i);
                    boxLine();
                    Console.WriteLine(friend.Fullname);
                }

                bool done = false;
                do
                {
                    boxLine();
                    Console.WriteLine("|| 1:Watch friends wall || 2: Back");
                    boxLine();
                    string key = Console.ReadLine();
                    if (string.IsNullOrEmpty(key)) continue;
                    switch (key[0])
                    {
                        case '1':
                        {
                            boxLine();
                            Console.WriteLine("Select friends number");
                            boxLine();
                            var index = Convert.ToInt32(Console.ReadLine()) - 1;
                            _view.Wall(_uService.GetById(_user.Following[index]).PersonalCircle);
                            break;
                        }
                        case '2':
                        {
                            done = true;
                            break;
                        }
                    }

                } while (!done);
            }
        }

        public void ShowGroups()
        {

        }

        public void SearchUser()
        {
            boxLine();
            Console.WriteLine("Enter name you wanna search for");
            boxLine();
            string name = Console.ReadLine();
            var names =_uService.SearchByName(name);
            foreach (var i in names)
            {
                Console.WriteLine(_uService.GetById(i).Fullname);
                boxLine();
            }

            bool done = false;
            do
            {
                boxLine();
                Console.WriteLine("|| 1:Show wall || 2:Show User details || 3:Go back ||");
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key)) continue;
                switch (key[0])
                {
                    case '1':
                    {   boxLine();
                        Console.WriteLine("Enter the number of the person you wanna watch");
                        int index = Convert.ToInt32(Console.ReadLine()) - 1;
                        var view = _view.Wall(_uService.GetById(names[index]).PersonalCircle);
                        Console.WriteLine("\n");
                        foreach (var i in view)
                        {
                            boxLine();
                            Console.WriteLine("Posted: " + i.date + " by: " + _uService.GetById(i.Owner_ID).Fullname);
                            boxLine();
                            Console.WriteLine(i.Content);
                            boxLine();
                            Console.WriteLine("\n");
                        }
                        break;
                    }
                    case '2':
                    {
                        boxLine();
                        Console.WriteLine("Enter the number of the person you wanna watch");
                        int index = Convert.ToInt32(Console.ReadLine()) - 1;
                        var person = _uService.GetById(names[index]);
                        boxLine();
                        Console.WriteLine("Name: " +person.Fullname);
                        Console.WriteLine("Age: " + person.Age);
                        Console.WriteLine("Gender: " + person.gender);
                        Console.WriteLine("Loggedin: " + person.Logged_in);
                        break;
                    }
                    case '3':
                    {
                        done = true;
                        break;
                    }
                }

            } while (!done);
        }

        public void SearchGroup()
        {

        }

        public void wall()
        {
            if (_user != null)
            {
                if (_user.Logged_in)
                {
                    var myPost = _view.Wall(_user.PersonalCircle);
                    Console.WriteLine("\n");
                    foreach (var i in myPost)
                    {
                        boxLine();
                        Console.WriteLine("Posted: " + i.date + " by: " + _uService.GetById(i.Owner_ID).Fullname);
                        boxLine();
                        Console.WriteLine(i.Content);
                        boxLine();
                        Console.WriteLine("\n");
                    }
                }
            }
            else
            Console.WriteLine("Please login");
        }

        public void feed()
        {
            if (_user != null && _user.Logged_in)
            {
                var myFeed = _view.Feed(_user);
                Console.WriteLine("\n");
                foreach (var i in myFeed)
                {
                    boxLine();
                    Console.WriteLine("Posted: " + i.date + " by: " + _uService.GetById(i.Owner_ID).Fullname);
                    boxLine();
                    Console.WriteLine(i.Content);
                    boxLine();
                    Console.WriteLine("\n");
                }
            }
            else
                Console.WriteLine("Please login");
   
        }

        public void logout()
        {
            _user.Logged_in = false;
            _uService.UpdateUser(_user);
        }
    }
}
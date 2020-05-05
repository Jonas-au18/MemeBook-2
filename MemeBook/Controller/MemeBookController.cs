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
            Console.WriteLine("|| e:Show friends|| h: Show groups ||");
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
                var user = _uService.GetById(refId);
                user.Logged_in = true;
                _uService.UpdateUser(user);
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
                            var index = Console.ReadLine();
                            _view.Wall(_uService.GetById(_user.Following[Convert.ToInt32(index)]).PersonalCircle);
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

            } while (!done);
        }

        public void SearchGroup()
        {

        }

        public void wall()
        {

        }

        public void feed()
        {

        }
    }
}
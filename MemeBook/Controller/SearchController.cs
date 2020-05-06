using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DnsClient;
using MemeBook.Models;
using MemeBook.Services;

namespace MemeBook.Controller
{
    public class SearchController
    {
        private LayoutControl _layout;
        private UserService _uService;
        private CircleService _cService;
        private WallControl _wall;
        private UserController _uController;
        private string _user = "";


        public SearchController(LayoutControl layout, UserService uServ, CircleService cServ,
            WallControl wall, UserController uControl)
        {
            _layout = layout;
            _uService = uServ;
            _cService = cServ;
            _wall = wall;
            _uController = uControl;
        }

        public void SearchUser(string id)
        {
            _user = id;
            _layout.boxLine();
            Console.WriteLine("Enter name you wanna search for");
            _layout.boxLine();
            string name = Console.ReadLine();
            var names = _uService.SearchByName(name);
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine("(" + i + ")" + _uService.GetById(names[i]).Fullname);
            }
            selectUser(names);
        }

        public void selectUser(List<string> ids)
        {
            _layout.boxLine();
            Console.WriteLine("|| 1:Show wall || 2:Show User details || 3:Follow user || 4:Block user || 5:Back ||");

            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
            switch (key[0])
            {
                case '1':
                {
                    int index = _layout.Getindex("Enter the number of the person you wanna watch");
                    _wall.showWall(ids[index],_user);
                    break;
                }
                case '2':
                {
                    int index = _layout.Getindex("Enter the number of the person you wanna watch");
                    _uController.showDetails(ids[index]);
                    break;
                }
                case '3':
                {
                    int index = _layout.Getindex("Enter the number of the person you want to follow");
                    if (!_uService.GetById(ids[index]).Blocked.Contains(_user))
                    {
                        _uService.followUser(_user,ids[index]);
                    }
                    break;
                }
                case '4':
                {
                    int index = _layout.Getindex("Enter the number of the person you want to block");
                    _uService.blockUser(_user,ids[index]);
                    break;
                }
                case '5':
                {
                    break;
                }
            }
        }

        public void SearchGroup(string id)
        {
            _user = id;
            _layout.boxLine();
            Console.WriteLine("Enter the group name you wanna search for");
            _layout.boxLine();
            string name = Console.ReadLine();
            var names = _cService.SearchByName(name);
            for (int i = 0; i < names.Count; i++)
            {
                if (!names[i].isPrivate ||names[i].AllowedUser.Contains(_user))
                {
                    _layout.boxLine();
                    Console.WriteLine("(" + i + ")Name: " + names[i].name + " Members: " + names[i].users.Count);
                    _layout.boxLine();
                }
                else
                {
                    _layout.boxLine();
                    Console.WriteLine("No such group exist");
                    _layout.boxLine();
                }
            }
            selectGroup(names);
        }

        public void selectGroup(List<Circles> circles)
        {
            _layout.boxLine();
            Console.WriteLine("|| 1:Show wall || 2:Join group || 3:Back ||");

            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
                switch (key[0])
                {
                    case '1':
                    {
                        int index = _layout.Getindex("Enter the number of the group you want to watch");
                        _wall.showCircleWall(_user, circles[index]);
                        break;
                    }
                    case '2':
                    {
                        int index = _layout.Getindex("Enter the number of the group you want to join");
                        _cService.AddToCircle(_user, circles[index].Circle_ID);
                        break;
                    }
                    case '3':
                    {
                        break;
                    }
                }
        }

    }
}

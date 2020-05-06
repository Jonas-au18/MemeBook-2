using System;
using System.Collections.Generic;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;

namespace MemeBook.Controller
{
    public class UserController
    {
        private UserService _uService;
        private CircleService _cService;
        private LayoutControl _layout;
        private SearchController _search;
        private WallControl _wall;
        private View _view;
        private string _id;

        public UserController(UserService uServ, CircleService cServ, LayoutControl layout,
            WallControl wall, View view)
        {
            _uService = uServ;
            _cService = cServ;
            _layout = layout;
            _wall = wall;
            _view = view;
        }

        public void showDetails(string id)
        {
            var person = _uService.GetById(id);
            _layout.boxLine();
            Console.WriteLine("Name: " + person.Fullname);
            Console.WriteLine("Age: " + person.Age);
            Console.WriteLine("Gender: " + person.gender);
            Console.WriteLine("Loggedin: " + person.Logged_in);
            _layout.boxLine();
        }

        public void showFollowing(string _user)
        {
            _id = _user;
            var owner = _uService.GetById(_user);
            for (int i = 0; i < owner.Following.Count; i++)
            {
                var following = _uService.GetById(owner.Following[i]).Fullname;
                _layout.boxLine();
                Console.WriteLine(i + " " + following);
            }
            _layout.boxLine();
            selector(owner.Following);
        }

        public void selector(List<string> ids)
        {
            _layout.boxLine();
            Console.WriteLine("|| 1:Watch following wall || 2: unfollow || 3:back ||");
            _layout.boxLine();
            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
            switch (key[0])
            {
                case '1':
                {
                    int index = _layout.Getindex("Select person to watch");
                    var myFollow = _uService.GetById(ids[index]);
                    _wall.showWall(ids[index],_id);
                    break;
                }
                case '2':
                {
                    int index = _layout.Getindex("Select person to unfollow");
                    var myFollow = _uService.GetById(ids[index]).User_ID;
                    _uService.unfollowUser(_id,myFollow);
                    break;
                }
                case '3':
                {
                    break;
                }
            }
        }

        public void showGroup(string _user)
        {
            _id = _user;
            var groups = _cService.FindByUser(_user);
            for (int i = 0; i < groups.Count; i++)
            {
                _layout.boxLine();
                Console.WriteLine(i + ")Name: " + groups[i].name + " Members: " + groups[i].users.Count + " Public: " + !groups[i].isPrivate);
                _layout.boxLine();
            }

           grpSelector(groups);
        }

        public void grpSelector(List<Circles> circles)
        {
            _layout.boxLine();
            Console.WriteLine("|| 1:Watch group wall || 2: leave group || 3:back ||");
            _layout.boxLine();
            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
                switch (key[0])
                {
                    case '1':
                    {
                        int index = _layout.Getindex("Select group wall to watch");
                        _wall.showCircleWall(_id,circles[index]);
                        break;
                    }
                    case '2':
                    {
                        int index = _layout.Getindex("Select group to leave");
                        _cService.RemoveFromCircle(_id,circles[index].Circle_ID);
                        break;
                    }
                    case '3':
                    {
                        break;
                    }
                }
        }

        public void feed(User user)
        {
            _id = user.User_ID;
            var myFeed = _view.Feed(user);
            {
                myFeed.Reverse();
                _layout.boxLine();
                for (int i = 0; i < myFeed.Count; i++)
                {
                    _layout.boxLine();
                    Console.WriteLine("(" + i + ")Posted: " + myFeed[i].date + " by: " + _uService.GetById(myFeed[i].Owner_ID).Fullname);
                    _layout.boxLine();
                    Console.WriteLine(myFeed[i].Content);
                    _layout.boxLine();

                }
            }
            _wall.select(myFeed,user.User_ID);
        }

    }

}
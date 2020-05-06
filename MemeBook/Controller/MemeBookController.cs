using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;
using MongoDB.Bson;
using MongoDB.Libmongocrypt;

namespace MemeBook.Controller
{
    public class MemeBookController
    {
        private UserService uServ;
        private CircleService cServ;
        private PostService pServ;
        private LayoutControl _layout;
        private View view;
        private Create create;
        private LoginController _login;
        private WallControl _wall;
        private UserController _uControl;
        private SearchController _search;
        private User _user = null;

        public MemeBookController(LayoutControl layout)
        {
            uServ = new UserService();
            cServ = new CircleService();
            pServ = new PostService(uServ);
            _layout = layout;
            view = new View(pServ,cServ,uServ);
            create = new Create(_layout,pServ);
            _login = new LoginController(_layout,new LoginService(), uServ);
            _wall = new WallControl(_layout,uServ,cServ,create,view);
            _uControl = new UserController(uServ,cServ,_layout,_wall,view);
            _search = new SearchController(_layout,uServ,cServ,_wall,_uControl);

        }

        public void isLogged()
        {
            _layout.boxLine();
            Console.WriteLine("|| w:View wall || f:View feed || u:Search user || g:Search group ||");
            Console.WriteLine("|| e:Following || h:Groups    || o:Logout      ||");
            _layout.boxLine();

            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
                switch (key[0])
                {
                    case 'w':
                    {
                        _wall.showCircleWall(_user.User_ID,_user.PersonalCircle);
                        break;
                    }
                    case 'f':
                    {
                        _uControl.feed(_user);
                        break;
                    }
                    case 'u':
                    {
                        _search.SearchUser(_user.User_ID);
                        break;
                    }
                    case 'g':
                    {
                        _search.SearchGroup(_user.User_ID);
                        break;
                    }
                    case 'e':
                    {
                        _uControl.showFollowing(_user.User_ID);
                        break;
                    }
                    case 'h':
                    {
                        _uControl.showGroup(_user.User_ID);
                        break;
                    }
                    case 'o':
                    {
                        _login.logout(_user);
                        break;
                    }
                }
        }

        public void NotLogged()
        {
            _layout.boxLine();
            Console.WriteLine("|| u:Search user || g:Search group || l:Login ||");
            _layout.boxLine();

            string key = Console.ReadLine();
            if (!string.IsNullOrEmpty(key))
                switch (key[0])
                {
                    case 'u':
                    {
                        _search.SearchUser("");
                        break;
                    }
                    case 'g':
                    {
                        _search.SearchGroup("");
                        break;
                    }
                    case 'l':
                    {
                        _user = _login.Login();
                        break;
                    }
                }
        }

        public void CheckLoginState()
        {
            if (_user != null && _user.Logged_in)
            {
                isLogged();
            }
            else
            {
                NotLogged();
            }
        }

    }
}
using System;
using System.Collections.Generic;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;
using MongoDB.Bson;

namespace MemeBook.Controller
{
    public class MemeBookController
    {
        private User _user = new User();
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
            if (_user != null && _user.Following != null)
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
            Console.WriteLine("Your're forever alone");
        }

        public void ShowGroups()
        {
            if (_user != null)
            {
                var grps = _cService.FindByUser(_user.User_ID);
                foreach (var i in grps)
                {
                    boxLine();
                    Console.WriteLine("Name: " + i.name + " Members: " + i.users.Count + " Public: " + !i.isPrivate);
                    boxLine();
                }
            }
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
                Console.WriteLine("|| 1:Show wall || 2:Show User details || 3:Follow user || 4:Block user || 5:Go back ||");
                string key = Console.ReadLine();
                if (string.IsNullOrEmpty(key)) continue;
                switch (key[0])
                {
                    case '1':
                    {   boxLine();
                        Console.WriteLine("Enter the number of the person you wanna watch");
                        int index = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (!_uService.GetById(names[index]).Blocked.Contains(_user.User_ID))
                        {
                            var view = _view.Wall(_uService.GetById(names[index]).PersonalCircle);
                            Console.WriteLine("\n");
                            foreach (var i in view)
                            {
                                boxLine();
                                Console.WriteLine(
                                    "Posted: " + i.date + " by: " + _uService.GetById(i.Owner_ID).Fullname);
                                boxLine();
                                Console.WriteLine(i.Content);
                                boxLine();
                                Console.WriteLine("\n");
                            }
                        }
                        else
                        {
                            boxLine();
                            Console.WriteLine("This user have blocked you");
                            boxLine();
                        }

                        break;
                    }
                    case '2':
                    {
                        boxLine();
                        Console.WriteLine("Enter the number of the person you wanna watch");
                        int index = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (!_uService.GetById(names[index]).Blocked.Contains(_user.User_ID))
                        {
                            var person = _uService.GetById(names[index]);
                            boxLine();
                            Console.WriteLine("Name: " + person.Fullname);
                            Console.WriteLine("Age: " + person.Age);
                            Console.WriteLine("Gender: " + person.gender);
                            Console.WriteLine("Loggedin: " + person.Logged_in);
                        }

                        break;
                    }
                    case '3':
                    {
                        boxLine();
                        Console.WriteLine("Enter the number of the person you want to follow");
                        boxLine();
                        int index = Convert.ToInt32(Console.ReadLine());
                        if (!_uService.GetById(names[index]).Blocked.Contains(_user.User_ID))
                        {
                            _uService.followUser(_user,names[index]);
                        }
                        break;
                    }
                    case '4':
                    {
                        boxLine();
                        Console.WriteLine("Enter the number of the person you want to block");
                        boxLine();
                        int index = Convert.ToInt32(Console.ReadLine());
                        _uService.blockUser(_user,names[index]);
                        break;
                    }
                    case '5':
                    {
                        done = true;
                        break;
                    }
                }

            } while (!done);
        }

        public void SearchGroup()
        {
            boxLine();
            Console.WriteLine("Enter the group name you wanna search for");
            boxLine();
            string name = Console.ReadLine();
            var names = _cService.SearchByName(name);
            foreach (var i in names)
            {
                if (!i.isPrivate ||i.AllowedUser.Contains(_user.User_ID))
                {
                    boxLine();
                    Console.WriteLine("Name: " + i.name + " Members: " + i.users.Count);
                    boxLine();
                }
                else
                {
                    boxLine();
                    Console.WriteLine("No such group exist");
                    boxLine();
                }
            }
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
            if (_user != null)
            {
                var myFeed = _view.Feed(_user);
                Console.WriteLine("\n");
                for (int i = myFeed.Count - 1; i > 0; i--)
                {
                    boxLine();
                    Console.WriteLine("Posted: " + myFeed[i].date + " by: " + _uService.GetById(myFeed[i].Owner_ID).Fullname);
                    boxLine();
                    Console.WriteLine(myFeed[i].Content);
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
            _user = null;
        }

        public void CreatePost(Circles circle)
        {
            boxLine();
            Console.WriteLine("Enter the data you want to post");
            boxLine();
            string content = Console.ReadLine();
            boxLine();
            Console.WriteLine("Is the post public or private [Y/N]");
            bool isPublic;
            if (Console.ReadLine() == "y")
            {
                isPublic = true;
            }
            else
            {
                isPublic = false;
            }

            Post myPost = new Post()
            {
                Owner_ID = _user.User_ID,
                Content = content,
                isPublic = isPublic,
                Circle_ID = circle.Circle_ID
            };
            _pService.CreatePost(myPost);
        }

        public void DeletePost(Circles circle)
        {
            boxLine();
            Console.WriteLine("Enter post you want to delete");
            boxLine();
            var posts = _pService.GetPostByCircle(circle);
            int index = Convert.ToInt32(Console.ReadLine());
            _pService.DeletePost(posts[index].Post_ID);
        }

        public void CreateComment(Post post)
        {
            boxLine();
            Console.WriteLine("Enter the comment you want to add");
            boxLine();
            string comment = Console.ReadLine();
            Comment newcomment = new Comment();
            newcomment.Owner_ID = _user.User_ID;
            newcomment.Content = comment;
            post.Comments.Add(newcomment);
            _pService.CreateComment(post.Post_ID,newcomment);
        }

        public void DeleteComment(int index, Post post)
        {
            boxLine();
            Console.WriteLine("Enter the number comment you want to remove");
            boxLine();
            int ind = Convert.ToInt32(Console.ReadLine());
            _pService.DeleteComment(ind,post);
        }
    }
}
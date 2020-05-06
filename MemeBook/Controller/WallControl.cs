using System;
using System.Collections.Generic;
using System.Text;
using MemeBook.Models;
using MemeBook.Queries;
using MemeBook.Services;

namespace MemeBook.Controller
{
    public class WallControl
    {
        private string myUser = null;
        private LayoutControl _layout;
        private UserService _uService;
        private CircleService _cService;
        private Create _create;
        private View _view;
        public WallControl(LayoutControl layout, UserService uServ,
            CircleService cServ, Create create, View view)
        {
            _layout = layout;
            _uService = uServ;
            _cService = cServ;
            _create = create;
            _view = view;
        }
        public void showWall(string user, string guest)
        {
            myUser = guest;
            var owner = _uService.GetById(user);
            if (guest == null || !owner.Blocked.Contains(guest))
            {
                var post = _view.Wall(user,guest);
                post.Reverse();
                for (int i = 0; i < post.Count; i++)
                {
                    _layout.boxLine();
                    Console.WriteLine("Post nr: " + (i + 1));
                    Console.WriteLine("Date: " + post[i].date + " By: " + _uService.GetById(post[i].Owner_ID).Fullname);
                    Console.WriteLine(post[i].Content);
                    Console.WriteLine("Nr. of comments:" + post[i].Comments.Count);
                    _layout.boxLine();
                }
                select(post,guest);
            }
        }

        public void select(List<Post> post,string user)
        {
            _layout.boxLine();
            Console.WriteLine("Select a post to choose from, els press x for back");
            _layout.boxLine();
            string input = Console.ReadLine();
            if (input != "x")
            {
                int index;
                if(Int32.TryParse(input, out index))
                {
                    if (index < 0)
                    {
                        index = 0;
                    }
                    var selected = post[index - 1];
                    options(selected,myUser);
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }
                
            }

        }

        public void options(Post post, string user)
        {
            _layout.boxLine();
            Console.WriteLine("|| 1:Show comments || 2: create post || 3:Post comment ||");
            string key = Console.ReadLine();
            switch (key[0])
            {
                case '1':
                {
                    showComment(post);
                    break;
                }
                case '2':
                { 
                    _create.CreatePost(_cService.FindByID(post.Circle_ID),myUser);
                    break;
                }
                case '3':
                {
                    _create.CreateComment(post,myUser);
                    break;
                }
            }
        }

        public void showComment(Post post)
        {
            if (post.Comments != null)
            {
                post.Comments.Reverse();
                foreach (var i in post.Comments)
                {
                    _layout.boxLine();
                    Console.WriteLine("Date " + i.date + " By: " + _uService.GetById(i.Owner_ID).Fullname);
                    Console.WriteLine(i.Content);
                    _layout.boxLine();
                }
            }
            else
            {
                _layout.boxLine();
                Console.WriteLine("Post have no comments, be the first the comment");
                _layout.boxLine();
            }
 
        }

        public void showCircleWall(string user,Circles circle)
        {
            myUser = user;
            var posts = _view.Wall(circle);
            posts.Reverse();
            for (int i = 0; i < posts.Count; i++)
            {
                _layout.boxLine();
                Console.WriteLine("Post nr: " + (i + 1));
                Console.WriteLine("Date: " + posts[i].date + " By: " + _uService.GetById(posts[i].Owner_ID).Fullname);
                Console.WriteLine(posts[i].Content);
                Console.WriteLine("Nr. of comments:" + posts[i].Comments.Count);
                _layout.boxLine();
            }
            select(posts,user);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MemeBook.Models;
using MemeBook.Services;
using Microsoft.VisualBasic;

namespace MemeBook.Queries
{
    public class View
    {
        private PostService _posts;
        private CircleService _Circles;
        private UserService _users;

        public View()
        {
            _posts = new PostService();
            _Circles = new CircleService();
            _users = new UserService();

        }

        public List<Post> Feed(User user)
        {
            List<Post> myFeed = new List<Post>();
            var myCircles = _Circles.FindByUser(user);

            foreach (var i in myCircles)
            {
                var post = _posts.GetPostByCircle(i);
                foreach (var j in post)
                {
                    myFeed.Add(j);
                }
            }

            return myFeed;
        }

        public List<Post> Wall(User user)
        {
            List<Post> myPosts = new List<Post>();
            var ids = _users.GetPostById(user.User_ID);
            Console.WriteLine(ids.Count);
            foreach (var i in ids)
            {
                myPosts.Add(_posts.GetPostById(i));
                Console.WriteLine("Adding post" + myPosts.Last());
            }
            return myPosts;
        }
    }
}
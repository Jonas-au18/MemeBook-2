using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using MemeBook.Models;
using MemeBook.Services;
using Microsoft.VisualBasic;
using MongoDB.Bson.Serialization.Serializers;

namespace MemeBook.Queries
{
    public class View
    {
        private PostService _posts;
        private CircleService _Circles;
        private UserService _users;

        public View(PostService post, CircleService circle, UserService user)
        {
            _posts = post;
            _Circles = circle;
            _users = user;

        }

        public List<Post> Feed(User user)
        {
            List<Post> myFeed = new List<Post>();
            var myCircles = _Circles.FindByUser(user.User_ID);

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

        public List<Post> Wall(Circles circle)
        {
            var ids = _posts.GetPostByCircle(circle);
            
            return ids;
        }

        public List<Post> Wall(string user, string guest)
        {
            var owner = _users.GetById(user);
            var posts = _posts.GetPostByUserId(owner.User_ID);
            List<Post> toSend = new List<Post>();
            foreach (var i in posts)
            {
                var circle = _Circles.FindByID(i.Circle_ID);
                if (!circle.isPrivate || circle.users.Contains(guest))
                {
                    toSend.Add(i);
                }
            }
                
            return toSend;

        }

    }
}